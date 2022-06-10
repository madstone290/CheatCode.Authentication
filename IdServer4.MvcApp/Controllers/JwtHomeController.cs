using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CheatCode.Authentication.Shared;

namespace CheatCode.Authentication.IdServer4.MvcApp.Controllers
{
    public class JwtHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "jwt")]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "myid111"),
                new Claim("granny", "cookie")
            };

            var secretBytes = Encoding.UTF8.GetBytes(SharedValues.IdServer4.Sercret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                claims: claims,
                issuer: SharedValues.IdServer4.Issuer,
                audience: SharedValues.IdServer4.Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials);
            

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
            
            return Ok(new { access_token = tokenJson });
        }
    }
}
