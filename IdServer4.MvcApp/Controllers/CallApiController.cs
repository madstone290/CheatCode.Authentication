using CheatCode.Authentication.Shared;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace CheatCode.Authentication.IdServer4.MvcApp.Controllers
{
    public class CallApiController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();

            string accessToken = await HttpContext.GetTokenAsync("access_token");
            client.SetBearerToken(accessToken);

            var response = client.GetAsync(SharedValues.IdServer4.ApiAddress + "/api/test").Result;
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

            var response = client.GetAsync(SharedValues.IdServer4.ApiAddress + "/api/test/read").Result;
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

            var response = client.GetAsync(SharedValues.IdServer4.ApiAddress + "/api/test/write").Result;
            var status = response.StatusCode;
            var content = response.Content.ReadAsStringAsync().Result;
            ViewData.Add("status", status);
            ViewData.Add("content", content);
            return View("Index");
        }

    }
}
