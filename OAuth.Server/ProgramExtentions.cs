using CheatCode.Authentication.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CheatCode.Authentication.OAuth.Server
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
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidAudience = SharedValues.OAuth.Audience,
                        ValidIssuer = SharedValues.OAuth.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SharedValues.OAuth.JwtSecret))
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            // 쿼리를 통해 인증할 수 있게 한다
                            if (context.Request.Query.ContainsKey("access_token"))
                            {
                                // 기본위치 (Authorization 헤더)가 아닌 곳에서 토큰을 설정할 수 있도록한다.
                                context.Token = context.Request.Query["access_token"];
                            }

                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            return Task.CompletedTask;
                        }
                    };
                });
        }

    }
}
