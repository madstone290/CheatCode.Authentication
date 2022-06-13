using IdentityServer4A.MvcApp.AuthorizationRequirements;
using IdentityServer4A.MvcApp.Data;
using IdentityServer4A.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using IdentityServer4A.Shared;

namespace IdentityServer4A.MvcApp
{
    public static class ProgramExtensions
    {
        public static void AddCheatCodeDbContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("Memory");
            });
        }

        public static void AddCheatCodeIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

            })
                //# Identity서비스를 사용할 DB 설정
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //# AddAuthentication설정을 덮어쓴다.
            services.ConfigureApplicationCookie(options =>
            {
                //# 기본값은 .AspNetCore.Identity.Application
                //options.Cookie.Name = "Identity.Cookie";

                options.AccessDeniedPath = "/Home/AccessDenied";
                options.LoginPath = "/MyHome/Login";
            });
        }

        public static void AddCheatCodeAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = Constants.CookiesScheme;

                    //# 권한이 요구될 떄 실행할 기본 스킴 설정
                    //# 권한요청 스킴이 없는 경우 기본 스킴의 로그인 페이지로 이동한다.
                    //options.DefaultChallengeScheme = SharedValues.OpenIdConnectScheme;

                    options.DefaultChallengeScheme = "OAuth";
                })
                .AddCookie(Constants.CookiesScheme, options =>
                {
                    //# 브라우저 쿠키명 설정. 기본값은 .AspNetCore.{스킴} ex) .AspNetCore.Cookies
                    //options.Cookie.Name = "MyGoodName";

                    //# 권한이 없을 경우 로그인 페이지로 이동한다. 기본값은 /Account/Login
                    options.LoginPath = "/MyHome/Login";
                })
                .AddJwtBearer("jwt", options => {
                    var secretBytes = Encoding.UTF8.GetBytes(Constants.Sercret);
                    var key = new SymmetricSecurityKey(secretBytes);

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Constants.Issuer,
                        ValidAudience = Constants.Audience,
                        IssuerSigningKey = key
                    };

                    options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents()
                    {
                        //# 쿼리에 포함된 토큰을 Jwt컨테스트의 토큰으로 설정한다
                        //# 테스트용으로 브라우저에서 쿼리로 토큰을 전달하기 위해 사용한다.
                        OnMessageReceived = (context) =>
                        {
                            if (context.Request.Query.ContainsKey("access_token"))
                            {
                                context.Token = context.Request.Query["access_token"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddOpenIdConnect(Constants.OpenIdConnectScheme, options =>
                {
                    //# OpenIdConnectHandler의 이벤트 핸들러 추가
                    options.Events = new Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents()
                    {
                        OnTokenValidated = async (context) =>
                        {
                        },
                        OnRedirectToIdentityProviderForSignOut = async (context) =>
                        {
                        }
                    };

                    options.Authority = Constants.IdServerAddress;
                    options.SignInScheme = Constants.CookiesScheme;

                    options.ClientId = Constants.MvcClientId;
                    options.ClientSecret = Constants.MvcClientSecret;

                    options.ResponseType = "code";
                    //options.ResponseType = "id_token";


                    //# 기본 scope제거
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("email");
                    options.Scope.Add("profile");
                    options.Scope.Add(Constants.OfficeResouceName);
                    options.Scope.Add(Constants.Api1Scope);
                    options.Scope.Add(Constants.CheatCodeApiReadScope);

                    //# 토큰을 저장하여 로그아웃할 떄 사용하도록 한다.
                    //# IdentityServer에서 확인없이 바로 로그아웃하기 위해서는 id_token이 필요하다.
                    //# 쿠키 사이즈를 줄이기 위해 별도의 로직을 이용해 id_token을 보관할 수 있다.
                    //# id_token을 어떻게 사용해야 하는가??
                    options.SaveTokens = true;

                    options.AccessDeniedPath = "/Home/AccessDenied";

                    //# 클레임을 획득하기 위해 엔드포인트에 한번 더 요청
                    //# id_token에 클레임을 포함시키지 않아 사이즈가 줄어든다.
                    options.GetClaimsFromUserInfoEndpoint = true;


                    //# 쿠키 클레임 맵핑
                    //# jwt에 포함된 값을 User.Claims에 추가한다.
                    options.ClaimActions.MapUniqueJsonKey("UserId", Constants.UserIdClaim);
                    options.ClaimActions.MapUniqueJsonKey("MyUserEmail", Constants.UserEmailClaim);
                });
        }

        public static void AddCheatCodeAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //# 기본 정책으로 인증된 사용자를 받는다.
                //# 여러 요구사항을 추가하여 기본 정책내용을 변경할 수 있다.
                var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                var defaultAuthPolicy = defaultAuthBuilder
                    .RequireAuthenticatedUser()
                    //.RequireClaim(ClaimTypes.DateOfBirth)
                    .Build();
                options.DefaultPolicy = defaultAuthPolicy;


                options.AddPolicy("Young", policy =>
                {
                    //# 요구되는 값 등록
                    policy.RequireClaim("Age", "10");
                });
                
                options.AddPolicy("Old", policy =>
                {
                    //# 요구조건 등록
                    policy.AddRequirements(new AgeRequirement(20));
                });
            });
            

            //# 핸들러를 등록하지 않으면 권한이 없는것으로 판단한다.
            services.AddScoped<IAuthorizationHandler, AgeRequirementHandler>();
        }




    }
}
