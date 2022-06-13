using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace OAuth.Api.Authentication
{
    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public CustomAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // 커스텀 인증

            // 인증 성공. Identity에 AuthenticationType이 null인경우 IsAuthenticated속성이 false이므로 값 설정할 것.
            //var principal = new System.Security.Claims.ClaimsPrincipal();
            //principal.AddIdentity(new System.Security.Claims.ClaimsIdentity("custom-id"));
            //var authTicket = new AuthenticationTicket(principal, "DefaultAuth");
            //return Task.FromResult(AuthenticateResult.Success(authTicket));

            // 인증 실패
            return Task.FromResult(AuthenticateResult.Fail("Failed Authentication"));
        }
    }
}
