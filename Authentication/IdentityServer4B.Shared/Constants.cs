namespace IdentityServer4B.Shared
{
    public class Constants
    {
        public const string ServerAddress = "https://localhost:5003";
        public const string ApiOneAddress = "https://localhost:6003";
        public const string ApiTwoAddress = "https://localhost:7003";
        public const string MvcClientAddress = "https://localhost:8003";
        public const string BlazorClientAddress = "https://localhost:9003";

        public const string ServerName = "Server";
        public const string ApiOneName = "Api1";
        public const string ApiTwoName = "Api2";

        public const string Bearer = "Bearer";
        public const string Cookie = "Cookie";
        public const string OpenIdConnect = "oidc";

        public const string ResponceType_Code = "code";


        public const string TokenSercret = "spijrofsjdioenlkvnuiweohuio4h98724yfe80f";

        public const string Client_1_Id = "client_id";
        public const string Client_1_Secret = "client_secret";

        public const string Client_2_Id = "client_id_mvc";
        public const string Client_2_Secret = "client_secret_mvc";


        public const string Client_3_Id = "client_id_blazor";
        public const string Client_3_Secret = "client_secret_blazor";

        public const string Scope_OfflineAccess = "offline_access";
        public const string Scope_OpenId = "openid";
        public const string Scope_Profile = "profile";
        public const string Scope_ApiOne = "ApiOne";
        public const string Scope_ApiTwo = "ApiTwo";

        public const string Scope_CustomClaim = "rc.scope";

        public const string Claim_Grandma = "rc.grandma";
        public const string Claim_ApiOne_UserGrade = "ApiOne.UserGrade";
        public const string Claim_ApiOne_UserId = "ApiOne.UserId";

    }
}