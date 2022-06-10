using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheatCode.Authentication.Shared
{
    public partial class SharedValues
    {
        public class OAuth
        {
            public const string JwtSecret = "sdfjoiejvni23bhh9u23y3498yurtufdhf";
            public const string ServerAddress = "https://localhost:5002";
            public const string ClientAddress = "https://localhost:7002";
            public const string ApiAddress = "https://localhost:6002";

            public const string Issuer = "Issuer";
            public const string Audience = "Audience";
        }
        
    }
}

