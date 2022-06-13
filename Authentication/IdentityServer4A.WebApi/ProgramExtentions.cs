using IdentityServer4A.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityServer4A.WebApi
{
    public static class ProgramExtentions
    {
        public static void AddCheatCodeAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = Constants.IdServerAddress;
                    options.Audience = Constants.CheatCodeName;

                    options.Events = new JwtBearerEvents()
                    {
                        OnTokenValidated = async (c) =>
                        {
                        }
                    };
                });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("read", policy =>
                {
                    //# 아래와 동일 policy.RequireClaim("scope", "CheatCode.Read");
                    policy.RequireScope(Constants.CheatCodeApiReadScope);
                });

                options.AddPolicy("write", policy =>
                {
                    policy.RequireScope(Constants.CheatCodeApiWriteScope);
                });
            });
        }
    }
}
