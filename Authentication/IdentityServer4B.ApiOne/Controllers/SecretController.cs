using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4B.ApiOne.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SecretController : ControllerBase
    {
        [Authorize]
        public string Get()
        {
            var claims = User.Claims.ToList();
            var claimsString = string.Join(", ", claims.Select(c => c.Type +":" + c.Value));
            return $"secret message from ApiOne with claims {claimsString}";
        }
    }
}
