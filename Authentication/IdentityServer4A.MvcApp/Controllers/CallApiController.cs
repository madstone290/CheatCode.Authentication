using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4A.Shared;

namespace IdentityServer4A.MvcApp.Controllers
{
    public class CallApiController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();

            string accessToken = await HttpContext.GetTokenAsync("access_token");
            client.SetBearerToken(accessToken);

            var response = client.GetAsync(Constants.ApiAddress + "/api/test").Result;
            var status = response.StatusCode;
            var content = response.Content.ReadAsStringAsync().Result;
            ViewData.Add("status", status);
            ViewData.Add("content", content);
            return View();
        }

        public async Task<IActionResult> Read()
        {
            HttpClient client = new HttpClient();

            string accessToken = await HttpContext.GetTokenAsync("access_token");
            client.SetBearerToken(accessToken);

            var response = client.GetAsync(Constants.ApiAddress + "/api/test/read").Result;
            var status = response.StatusCode;
            var content = response.Content.ReadAsStringAsync().Result;
            ViewData.Add("status", status);
            ViewData.Add("content", content);
            return View("Index");
        }


        public async Task<IActionResult> Write()
        {
            HttpClient client = new HttpClient();

            string accessToken = await HttpContext.GetTokenAsync("access_token");
            client.SetBearerToken(accessToken);

            var response = client.GetAsync(Constants.ApiAddress + "/api/test/write").Result;
            var status = response.StatusCode;
            var content = response.Content.ReadAsStringAsync().Result;
            ViewData.Add("status", status);
            ViewData.Add("content", content);
            return View("Index");
        }

    }
}
