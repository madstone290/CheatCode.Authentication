using CheatCode.Authentication.OAuth.Api.Authentication;
using CheatCode.Authentication.OAuth.Api.Authorization;
using CheatCode.Authentication.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CheatCode.Authentication.OAuth.Api
{
    public static class ProgramExtensions
    {
        public static void AddCheatCodeAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication("DefaultAuth")
                .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("DefaultAuth", null);
        }

        public static void AddCheatCodeAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                var defaultPolicy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new JwtRequirement())
                    .Build();
                options.DefaultPolicy = defaultPolicy;
            });

            services.AddScoped<IAuthorizationHandler, JwtRequirementHandler>();
        }

    }
}
