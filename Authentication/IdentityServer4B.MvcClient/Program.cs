using IdentityServer4B.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = Constants.Cookie;
    options.DefaultChallengeScheme = Constants.OpenIdConnect;
})
    .AddCookie(Constants.Cookie, options =>
    {
        options.Cookie.Name = "IdentityServer.MvcClient.Cookie";
    })
    .AddOpenIdConnect(Constants.OpenIdConnect, options =>
    {
        options.Authority = Constants.ServerAddress;
        options.ClientId = Constants.Client_2_Id;
        options.ClientSecret = Constants.Client_2_Secret;
        options.SaveTokens = true;
 
        options.ResponseType = Constants.ResponceType_Code;

        // cookie claim 맵핑 설정
        // 특정 claim을 삭제할 수 있다.
        options.ClaimActions.DeleteClaim("amr");
        // 사용자정보를 조회할 때 사용자 정보 json에서 지정한 키값을 추출해서 cookie claim으로 복사한다.
        // OpenIdConnectOptions.GetClaimsFromUserInfoEndpoint속성이 true일떄 적용된다.
        options.ClaimActions.MapUniqueJsonKey(Constants.Claim_Grandma, Constants.Claim_Grandma);

        // id_token에 사용자 정보를 포함시키지 않고
        // 사용자 정보를 조회하기 위해 endpoint에 한번더 다녀온다.
        options.GetClaimsFromUserInfoEndpoint = true;

        options.Scope.Add(Constants.Scope_OpenId);
        options.Scope.Add(Constants.Scope_ApiOne);
        options.Scope.Add(Constants.Scope_ApiTwo);
        options.Scope.Add(Constants.Scope_CustomClaim);
        options.Scope.Add(Constants.Scope_OfflineAccess);


        options.Events = new OpenIdConnectEvents()
        {
            OnUserInformationReceived = c =>
            {
                // *** 쿠키크기와 관련된 문제가 발생하지 않는한 OpenIdConnectOptions.SaveTokens = true를 사용할 것 ***
                // OpenIdConnectOptions.SaveTokens = true 와 같은 역할
                // SaveTokens에는 총 6개의 토큰이 포함된다.
                // AuthenticationProeperties에 토큰값을 저장한다.
                // 필요한 토큰만 추가한다.
                // **주의** 빈 배열을 전달하는 경우 모든 토큰이 삭제된다

                //var saveTokens = new AuthenticationToken[]
                //{
                //    new AuthenticationToken
                //    {
                //        Name = "id_token",
                //        Value = c.ProtocolMessage.IdToken
                //    },
                //    new AuthenticationToken
                //    {
                //        Name = "access_token",
                //        Value = c.ProtocolMessage.AccessToken
                //    },
                //    new AuthenticationToken
                //    {
                //        Name = "refresh_token",
                //        Value = c.ProtocolMessage.RefreshToken
                //    }
                //};
                //c.Properties.StoreTokens(saveTokens);
                return Task.CompletedTask;
            },
            OnTokenResponseReceived = c =>
            {
                return Task.CompletedTask;
            },
            OnTokenValidated = c =>
            {
               
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
