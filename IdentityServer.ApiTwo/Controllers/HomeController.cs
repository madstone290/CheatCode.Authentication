using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using CheatCode.Authentication.Shared;

namespace IdentityServer.ApiTwo.Controllers
{
    [Route("Home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //retrieve access token
            var serverClient = _httpClientFactory.CreateClient();

            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync(SharedValues.IdentityServer.ServerAddress);

            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,

                    ClientId = SharedValues.IdentityServer.Client_1_Id,
                    ClientSecret = SharedValues.IdentityServer.Client_1_Secret,

                    Scope = SharedValues.IdentityServer.Scope_ApiOne
                });

            //retrieve secret data
            var apiClient = _httpClientFactory.CreateClient();

            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync(SharedValues.IdentityServer.ApiOneAddress +"/secret");

            var content = await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                access_token = tokenResponse.AccessToken,
                message = content,
            });
        }
    }
}
