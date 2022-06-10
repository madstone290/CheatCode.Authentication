using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CheatCode.Authentication.Shared;

namespace CheatCode.Authentication.OAuth.Server.Controllers
{
    public class OAuthController : Controller
    {
        [Authorize]
        public IActionResult Validate()
        {
            if(HttpContext.Request.Query.TryGetValue("access_token", out var accessToken) == false)
                return BadRequest();


            return Ok();
        }

        [HttpGet]
        public IActionResult Authorize(
            [FromQuery] string response_type, // authorization flow type
            [FromQuery] string client_id, // client id
            [FromQuery] string redirect_uri, // 
            [FromQuery] string scope, // what info I want = email, phone
            [FromQuery] string state) // random string to confirm that we are going back to the same client
        {
            // Client에서 Get요청이 온다
            // 쿼리 파라미터를 읽고 인증 절차 진행
            var query = new QueryBuilder();
            query.Add("redirect_uri", redirect_uri);
            query.Add("state", state);

            return View(model: query.ToString());
        }

        [HttpPost]
        public IActionResult Authorize(
            [FromForm] string userName,
            [FromQuery] string redirect_uri,
            [FromQuery] string state)
        {
            // 사용자 인증을 진행한다
            // 사용자가 유효하면 코드를 발급한다

            string code = "sdfsdfdf";

            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);

            return Redirect($"{redirect_uri}{query}");
        }

        public IActionResult Token(
            [FromForm] string grant_type, // flow of access_token request
            [FromForm] string code, // 인증 확인
            [FromForm] string redirect_uri,
            [FromForm] string client_id,
            [FromForm] string refresh_token
            )
        {
            // 코드 유효성을 검증한다
            // 코드가 유효한 경우 토큰을 발급한다.


            var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
                new Claim("granny", "cookie")
            };

            var secretBytes = Encoding.UTF8.GetBytes(SharedValues.OAuth.JwtSecret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);


            // refresh_token 테스트.
            // 액세스토큰 만료를 1밀리초로 하여 리프레시전에는 사용이 불가하다
            var token = new JwtSecurityToken(
                SharedValues.OAuth.Issuer,
                SharedValues.OAuth.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: grant_type == "refresh_token" 
                    ? DateTime.Now.AddMinutes(5)
                    : DateTime.Now.AddMilliseconds(1),
                signingCredentials);

            var access_token = new JwtSecurityTokenHandler().WriteToken(token);

            var responseObject = new
            {
                access_token,
                token_type = "Bearer",
                raw_claim = "oauthTutorial",
                refresh_token = "RefreshTokenSampleValueSomething77"
            };

            return Ok(responseObject);
        }

    }
}
