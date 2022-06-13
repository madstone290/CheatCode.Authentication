using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    // .Authenticate() 할때
    options.DefaultAuthenticateScheme = "ClientCookie";
    // .SignInAsync() 할때
    options.DefaultSignInScheme = "ClientCookie";
    // 권한 확인할 때. [Authorize]특성이 적용될 때
    options.DefaultChallengeScheme = "OAuth";
})
    .AddCookie("ClientCookie", options =>
    {
        options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents()
        {
            OnSignedIn = (context) =>
            {
                return Task.CompletedTask;
            }
        };
    })
    .AddOAuth("OAuth", options =>
    { 
        // code를 발급받을 곳
        options.AuthorizationEndpoint = "https://localhost:5002/OAuth/authorize";
        // 토큰을 발급받을 곳
        options.TokenEndpoint = "https://localhost:5002/OAuth/token";
        options.ClientId = "id";
        options.ClientSecret = "secret";
        // 콜백주소. 이 주소로 요청이 들어오면 미들웨어에서 처리한다.
        options.CallbackPath = "/oauth/callback";

        options.SaveTokens = true;

        options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents()
        {
            OnCreatingTicket = context =>
            {
                // jwt에 포함된 클레임을 Identity 클레임에 추가한다
                var base64Payload = context.AccessToken.Split('.')[1];
                var bytes = Base64UrlEncoder.DecodeBytes(base64Payload);
                string payload = Encoding.UTF8.GetString(bytes);
                var claims = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload);

                foreach (var claim in claims)
                {
                    context.Identity.AddClaim(new Claim(claim.Key, claim.Value));
                }

                return Task.CompletedTask; 
            }
        };
    });


var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
