using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4B.Shared;

namespace IdentityServer4B.Server
{
    public static class Configuration
    {
        // id_token 및 access_token에 포함되는 claim
        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),

            // id_token에 추가하고자 하는 claim이 존재할 경우 IdentityResource를 미리 등록해야한다.
            // IdentityResources 컬렉션에 등록된 claim만이 id_token에 포함될 수 있다.
            new IdentityResource
            {
                Name = Constants.Scope_CustomClaim,
                UserClaims =
                {
                    Constants.Claim_Grandma
                }
            }
        };

        public static IEnumerable<ApiScope> GetScopes() => new ApiScope[]
        {
            new ApiScope(Constants.Scope_ApiOne),
            new ApiScope(Constants.Scope_ApiTwo),
        };

        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource> 
        {
            new ApiResource(Constants.ApiOneName)
            {
                // 클레임을 access_token에 자동으로 등록한다
                UserClaims = new string[]
                {
                    Constants.Claim_ApiOne_UserGrade,
                    Constants.Claim_ApiOne_UserId,
                },
                Scopes = new[]{ Constants.Scope_ApiOne },
            },
            new ApiResource(Constants.ApiTwoName)
            {
                Scopes = new []{ Constants.Scope_ApiTwo }
            }
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            // Client1
            new Client
            {
                ClientId = Constants.Client_1_Id,
                ClientSecrets = { new Secret(Constants.Client_1_Secret.ToSha256()) },

                // ClientCredentials: 사용자 상호작용없이 클라이언트 Id/Pw로 인증 진행
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                AllowedScopes = { Constants.Scope_ApiOne }
            },
            // Client2
            new Client
            {
                ClientId =  Constants.Client_2_Id,
                ClientSecrets = { new Secret(Constants.Client_2_Secret.ToSha256()) },

                // AuthorizationCode -> Token 프로세스
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,

                // "/signin-oidc: openidconnect 미들웨어의 기본 리디렉트 주소
                RedirectUris = {  Constants.MvcClientAddress + "/signin-oidc" },

                //# 외부에서 로그아웃되었을 때 클라이언트 로그아웃을 실행할 경로 설정
                //# OpenIdConnectHandler는 Front Channel이다?
                //BackChannelLogoutUri = SharedValues.MvcClientAddress + "/signout-oidc",
                FrontChannelLogoutUri = Constants.MvcClientAddress + "/signout-oidc",

                //# /signout-callback-oidc : OpenIdConnectHandler 경로. 로그아웃 콜백을 실행한다.
                PostLogoutRedirectUris =  { Constants.MvcClientAddress + "/signout-callback-oidc" },
                //PostLogoutRedirectUris = { Constants.MvcClientAddress + "/Home/Index" },



                AllowedScopes = {

                    Constants.Scope_OpenId,
                    Constants.Scope_Profile,
                    Constants.Scope_ApiOne,
                    Constants.Scope_ApiTwo,
                    Constants.Scope_CustomClaim,
                },

                // user identity에 존재하는 claim을 id_token에 포함시킨다.
                //AlwaysIncludeUserClaimsInIdToken = true,
                
                // refresh_token을 요청할 수 있도록 한다.
                AllowOfflineAccess = true,

                RequireConsent = false,
            },
               new Client
            {
                ClientId =  Constants.Client_3_Id,
                ClientSecrets = { new Secret(Constants.Client_3_Secret.ToSha256()) },

                // AuthorizationCode -> Token 프로세스
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,

                // "/signin-oidc: openidconnect 미들웨어의 기본 리디렉트 주소
                RedirectUris = {  Constants.BlazorClientAddress + "/signin-oidc" },

                //# 외부에서 로그아웃되었을 때 클라이언트 로그아웃을 실행할 경로 설정
                //# OpenIdConnectHandler는 Front Channel이다?
                //BackChannelLogoutUri = SharedValues.MvcClientAddress + "/signout-oidc",
                FrontChannelLogoutUri = Constants.BlazorClientAddress + "/signout-oidc",

                //# /signout-callback-oidc : OpenIdConnectHandler 경로. 로그아웃 콜백을 실행한다.
                PostLogoutRedirectUris =  { Constants.BlazorClientAddress + "/signout-callback-oidc" },
                //PostLogoutRedirectUris = { Constants.MvcClientAddress + "/Home/Index" },



                AllowedScopes = {

                    Constants.Scope_OpenId,
                    Constants.Scope_Profile,
                    Constants.Scope_ApiOne,
                    Constants.Scope_ApiTwo,
                    Constants.Scope_CustomClaim,
                },

                // user identity에 존재하는 claim을 id_token에 포함시킨다.
                //AlwaysIncludeUserClaimsInIdToken = true,
                
                // refresh_token을 요청할 수 있도록 한다.
                AllowOfflineAccess = true,

                RequireConsent = false,
            },

            //new Client
            //{
            //    ClientId = "angular",

            //    AllowedGrantTypes = GrantTypes.Code,
            //    RequirePkce = true,
            //    RequireClientSecret = false,

            //    RedirectUris = { "http://localhost:4200" },
            //    PostLogoutRedirectUris = { "http://localhost:4200" },
            //    AllowedCorsOrigins = { "http://localhost:4200" },

            //    AllowedScopes = {
            //        IdentityServerConstants.StandardScopes.OpenId,
            //        "ApiOne",
            //    },

            //    AllowAccessTokensViaBrowser = true,
            //    RequireConsent = false,
            //},

            //new Client
            //{
            //    ClientId = "wpf",

            //    AllowedGrantTypes = GrantTypes.Code,
            //    RequirePkce = true,
            //    RequireClientSecret = false,

            //    RedirectUris = { "http://localhost/sample-wpf-app" },
            //    AllowedCorsOrigins = { "http://localhost" },

            //    AllowedScopes = {
            //        IdentityServerConstants.StandardScopes.OpenId,
            //        "ApiOne",
            //    },

            //    AllowAccessTokensViaBrowser = true,
            //    RequireConsent = false,
            //},
            //new Client {
            //    ClientId = "xamarin",

            //    AllowedGrantTypes = GrantTypes.Code,
            //    RequirePkce = true,
            //    RequireClientSecret = false,

            //    RedirectUris = { "xamarinformsclients://callback" },

            //    AllowedScopes = {
            //        IdentityServerConstants.StandardScopes.OpenId,
            //        "ApiOne",
            //    },

            //    AllowAccessTokensViaBrowser = true,
            //    RequireConsent = false,
            //},
            //new Client {
            //        ClientId = "flutter",

            //        AllowedGrantTypes = GrantTypes.Code,
            //        RequirePkce = true,
            //        RequireClientSecret = false,

            //        RedirectUris = { "http://localhost:4000/" },
            //        AllowedCorsOrigins = { "http://localhost:4000" },

            //        AllowedScopes = {
            //            IdentityServerConstants.StandardScopes.OpenId,
            //            "ApiOne",
            //        },

            //        AllowAccessTokensViaBrowser = true,
            //        RequireConsent = false,
            //    },

            };
    }

}