using CheatCode.Authentication.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CheatCode.Authentication.IdServer4.WebApi
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
                    options.Authority = SharedValues.IdServer4.IdServerAddress;
                    options.Audience = SharedValues.IdServer4.CheatCodeName;

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
                    policy.RequireScope(SharedValues.IdServer4.CheatCodeApiReadScope);
                });

                options.AddPolicy("write", policy =>
                {
                    policy.RequireScope(SharedValues.IdServer4.CheatCodeApiWriteScope);
                });
            });
        }
    }
}
