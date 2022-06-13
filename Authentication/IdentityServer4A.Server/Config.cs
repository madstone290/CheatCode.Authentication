using IdentityServer4A.Shared;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;
using System.Text.Json;

namespace IdentityServer4A.Server
{
    /// <summary>
    /// Identity Server4 설정
    /// </summary>
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
             //# IdentityResource는 id_token에만 포함된다.
            new IdentityResource()
            {
                Name = Constants.OfficeResouceName,
                UserClaims = { Constants.OfficeNumberClaim }
            },
           
        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            //# ApiScope에 등록된 scope만 추가할 수 있다.
            //# ApiResource를 추가함으로써 JWT에 aud(audience) 항목이 추가된다
            //# Api서버에서 aud를 확인함으로써 좀 더 높은 보안을 유지할 수 있다.
            new ApiResource(Constants.CheatCodeName, "CheatCode Api")
            {
                //# API에서 사용하는 스코프. 해당 스코프를 지정하면 aud가 현재 API resource로 지정된다.
                Scopes = { Constants.CheatCodeApiReadScope, Constants.CheatCodeApiWriteScope },
                //# API에서 제공하는 스코프를 사용할 경우 access_token에 클레임이 포함된다.
                UserClaims = { Constants.UserIdClaim , Constants.UserEmailClaim }
            }
        };


        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope(Constants.Api1Scope, Constants.Api1Scope),
            new ApiScope(Constants.Api2Scope, Constants.Api2Scope),
            new ApiScope( Constants.CheatCodeApiReadScope,  Constants.CheatCodeApiReadScope),
            new ApiScope( Constants.CheatCodeApiWriteScope,  Constants.CheatCodeApiWriteScope),
        };


        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client()
            {
                ClientName = "Mvc Client",
                ClientId = Constants.MvcClientId,
                ClientSecrets =
                {
                    new Secret(Constants.MvcClientSecret.Sha256())
                },
                //# code ResponseType사용
                //AllowedGrantTypes = GrantTypes.Code,
                AllowedGrantTypes = GrantTypes.Code,
                //# /signin-oidc : OpenIdConnectHandler가 위치한 경로. 로그인 콜백을 실행한다.
                RedirectUris = {  Constants.MvcClientAddress + "/signin-oidc" },

                //# 외부에서 로그아웃되었을 때 클라이언트 로그아웃을 실행할 경로 설정
                //# OpenIdConnectHandler는 Front Channel이다?
                //BackChannelLogoutUri = SharedValues.MvcClientAddress + "/signout-oidc",
                FrontChannelLogoutUri = Constants.MvcClientAddress + "/signout-oidc",

                //# /signout-callback-oidc : OpenIdConnectHandler 경로. 로그아웃 콜백을 실행한다.
                PostLogoutRedirectUris =  { Constants.MvcClientAddress + "/signout-callback-oidc" },

                AllowedScopes = { "openid", "email", "profile", 
                    Constants.OfficeResouceName,
                    Constants.Api1Scope,
                    Constants.CheatCodeApiReadScope
                },

                //# 권한 동의 사용하기 
                RequireConsent = false,

                //# true인 경우 id_token에 사용자 클레임이 추가된다.
                //# IProfileService에서 추가한 클레임이 토큰에 포함되도록 한다.
                //# 클라이언트가 요청할 때 
                AlwaysIncludeUserClaimsInIdToken = false,

            }
        };

        static readonly dynamic address = new
        {
            street_address = "One Hacker Way",
            locality = "Heidelberg",
            postal_code = 69118,
            country = "Germany"
        };

        public static IEnumerable<TestUser> Users => new TestUser[]
        {
            new TestUser
            {
                SubjectId = "818727",
                Username = "alice",
                Password = "alice",
                Claims =
                {
                    //# 커스텀 Id Resource에 사용된다
                    new Claim(Constants.OfficeNumberClaim, "25"),
                    new Claim(Constants.UserIdClaim, "123"),
                    new Claim(Constants.UserEmailClaim, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                }
            },
            new TestUser
            {
                SubjectId = "88421113",
                Username = "bob",
                Password = "bob",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Bob Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Bob"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                    new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                }
            }
        };




    }
}
