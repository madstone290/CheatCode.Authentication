using CheatCode.Authentication.IdServer4.IdServer.Custom;
using CheatCode.Authentication.Shared;
using IdentityServer4;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CheatCode.Authentication.IdServer4.IdServer
{
    public static class ProgramExtensions
    {
        public static void AddCheatCodeIdentityServer(this IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddTestUsers(Config.Users.ToList())
                //.AddProfileService<ProfileService>()
                //# 실제 TLS 인증서 사용할 것
                //.AddSigningCredential("CertificateName")
                // not recommended for production - you need to store your key material somewhere secure
                .AddDeveloperSigningCredential();
        }


        public static void AddCheatCodeAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    //# 인증파일 다운받은 후 CheatCodeGoogleSecret.json으로 파일 위치할 것
                    //# link: https://drive.google.com/file/d/1DCfTWaSCUcCloaf-36jYovJAN38LF4Np/view?usp=sharing 

                    var jObject = JObject.Parse(File.ReadAllText("Secrets/CheatCodeGoogleSecret.json"));
                    options.ClientId = jObject.GetValue("ClientId").ToString();
                    options.ClientSecret = jObject.GetValue("ClientSecret").ToString();

                    // Google Console에 등록된 Redirect Uri와 동일해야한다. 실제로 리디렉트 되진 않는다..
                    options.CallbackPath = "/Account/Login/Google/Callback";

                    options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents()
                    {
                        OnTicketReceived = async (context) =>
                        {
                            context.Principal.Identities.First().AddClaim(
                                new Claim(SharedValues.IdServer4.UserIdClaim, "From Google"));
                            context.Principal.Identities.First().AddClaim(
                                new Claim(SharedValues.IdServer4.UserEmailClaim, "email@MyEmail.com"));
                        }
                    };

                });
        }
    }
}
