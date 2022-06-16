using IdentityServer4B.Server;
using IdentityServer4B.Server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Mappers;
using System.Security.Cryptography.X509Certificates;
using IdentityServer4;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using IdentityServer4B.Server.Identity;
using IdentityServer4B.Shared;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("LocalDb");

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    //options.UseInMemoryDatabase("Memory");
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddUserManager<ApplicationUserManager>()
    .AddUserStore<ApplicationUserStore>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddScoped<IUserStore<ApplicationUser>, ApplicationUserStore>();

// Identity서비스의 앱 쿠키 설정
// AddAuthentication 값을 덮어쓴다.
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "IdentityServer.Server.Cookie";
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
});

var assembly = typeof(AppIdentityDbContext).Assembly.GetName().Name;

//var filePath = Path.Combine(builder.Environment.ContentRootPath, "is_cert_secret.pfx");
//var certificate = new X509Certificate2(filePath, "password");

builder.Services.AddIdentityServer(options =>
{
})
    .AddAspNetIdentity<ApplicationUser>() // IdentityUser -> Token 변환을 도와준다 
    .AddConfigurationStore<AppConfigurationDbContext>(options =>
    {
        options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(assembly));
    })
    .AddOperationalStore<AppPersistedGrantDbContext>(options =>
    {
        options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(assembly));
        // 오래된 토큰을 DB에서 삭제한다.
        options.EnableTokenCleanup = true;
    })
    //.AddInMemoryIdentityResources(Configuration.GetIdentityResources())
    //.AddInMemoryApiScopes(Configuration.GetScopes())
    //.AddInMemoryApiResources(Configuration.GetApis())
    //.AddInMemoryClients(Configuration.GetClients())
    //.AddSigningCredential(certificate);
    .AddDeveloperSigningCredential();

builder.Services.AddAuthentication()
    .AddGoogle("Google", options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

        //# 인증파일 다운받은 후 CheatCodeGoogleSecret.json으로 파일 위치할 것
        //# link: https://drive.google.com/file/d/1DCfTWaSCUcCloaf-36jYovJAN38LF4Np/view?usp=sharing 

        var jObject = JObject.Parse(File.ReadAllText("Secrets/CheatCodeGoogleSecret.json"));
        options.ClientId = jObject.GetValue("ClientId").ToString();
        options.ClientSecret = jObject.GetValue("ClientSecret").ToString();

        // Google Console에 등록된 Redirect Uri와 동일해야한다. 미들웨어가 처리한다.
        options.CallbackPath = "/Account/Login/Google/Callback";

        options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents()
        {
            OnTicketReceived = async (context) =>
            {
                // 클레임 추가 
                //context.Principal.Identities.First().AddClaim(
                //    new Claim(SharedValues.IdServer4.UserIdClaim, "From Google"));
            }
        };

    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

// 시드데이터 추가
using(var scope = app.Services.CreateScope())
{
    // 기본 사용자 등록
    var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
    var user = await userManager.FindByNameAsync("bob");
    if(user == null)
    {
        user = new ApplicationUser() 
        {
            UserName ="bob",
            DisplayUserName = "I am bob", 
            Deleted = true 
        };

        var result = userManager.CreateAsync(user, "bob").GetAwaiter().GetResult();

        userManager.AddClaimAsync(user, new System.Security.Claims.Claim(Constants.Claim_Grandma, "big.cookie"))
            .GetAwaiter().GetResult();


        // 아래 두개의 클레임을 access_token에 포함시키기 위해서는 
        // ApiResouce에 UserClaim을 추가해야 한다.
        userManager.AddClaimAsync(user, new System.Security.Claims.Claim(Constants.Claim_ApiOne_UserId, "Bruce"))
            .GetAwaiter().GetResult();
        userManager.AddClaimAsync(user, new System.Security.Claims.Claim(Constants.Claim_ApiOne_UserGrade, "Admin"))
            .GetAwaiter().GetResult();
    }

    // IdentityServer4 시드데이터 추가
    scope.ServiceProvider.GetRequiredService<AppPersistedGrantDbContext>().Database.Migrate();

    var context = scope.ServiceProvider.GetRequiredService<AppConfigurationDbContext>();
    context.Database.Migrate();

    //모든 로우 삭제
    context.Clients.RemoveRange(context.Clients.ToArray());
    context.ClientCorsOrigins.RemoveRange(context.ClientCorsOrigins.ToArray());
    context.IdentityResources.RemoveRange(context.IdentityResources.ToArray());
    context.ApiScopes.RemoveRange(context.ApiScopes.ToArray());
    context.ApiResources.RemoveRange(context.ApiResources.ToArray());
    context.SaveChanges();

    if (!context.Clients.Any())
    {
        foreach (var client in Configuration.GetClients())
        {
            context.Clients.Add(client.ToEntity());
        }
        context.SaveChanges();
    }

    if (!context.IdentityResources.Any())
    {
        foreach (var resource in Configuration.GetIdentityResources())
        {
            context.IdentityResources.Add(resource.ToEntity());
        }
        context.SaveChanges();
    }

    if (!context.ApiScopes.Any())
    {
        foreach (var resource in Configuration.GetScopes())
        {
            context.ApiScopes.Add(resource.ToEntity());
        }
        context.SaveChanges();
    }

    if (!context.ApiResources.Any())
    {
        foreach (var resource in Configuration.GetApis())
        {
            context.ApiResources.Add(resource.ToEntity());
        }
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
