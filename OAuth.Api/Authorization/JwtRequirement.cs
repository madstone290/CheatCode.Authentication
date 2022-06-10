using Microsoft.AspNetCore.Authorization;
using CheatCode.Authentication.Shared;

namespace CheatCode.Authentication.OAuth.Api.Authorization
{
    public class JwtRequirement : IAuthorizationRequirement
    {
    }

    public class JwtRequirementHandler : AuthorizationHandler<JwtRequirement>
    {
        private readonly HttpClient _httpClient;
        private readonly HttpContext _httpContext;

        public JwtRequirementHandler(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpContext = httpContextAccessor.HttpContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, JwtRequirement requirement)
        {
            if (_httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader) == false)
                return;

            var accessToken = authHeader.ToString().Split(' ')[1];

            // 쿼리를 이용해 jwt인증을 진행한다.
            var response = await _httpClient.GetAsync($"{SharedValues.OAuth.ServerAddress}/oauth/validate?access_token={accessToken}");


            if (response.IsSuccessStatusCode)
                context.Succeed(requirement);
        }
    }

}
