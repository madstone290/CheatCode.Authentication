﻿namespace CheatCode.Authentication.Shared
{
    /// <summary>
    /// Authorization(IdentityServer4), Client(Web Application), Resource(Api) 사이에서 사용되는 값.
    /// Production버전에서는 별도로 관리할 것.
    /// </summary>
    public partial class SharedValues
    {
        public class IdServer4
        {
            public static string Issuer = "Me";
            public static string Audience = "Me";
            public static string Sercret = "spijrofsjdioenlkvnuiweohuio4h98724yfe80f";

            public const string CookiesScheme = "Cookies";
            public const string OpenIdConnectScheme = "oidc";


            public const string MvcClientId = "mvc";
            public const string MvcClientSecret = "mvc_pw";

            public const string MvcClientAddress = "https://localhost:7001";
            public const string ApiAddress = "https://localhost:6001";
            public const string IdServerAddress = "https://localhost:5001";

            public const string OfficeResouceName = "office";
            public const string OfficeNumberClaim = "office_number";

            public const string UserEmailClaim = "UserEmail";
            public const string UserIdClaim = "UserId";

            public const string Api1Scope = "Api1";
            public const string Api2Scope = "Api2";

            public const string CheatCodeName = "CheatCode";
            public const string CheatCodeApiReadScope = "CheatCode.Read";
            public const string CheatCodeApiWriteScope = "CheatCode.Write";
        }
       
    }
}