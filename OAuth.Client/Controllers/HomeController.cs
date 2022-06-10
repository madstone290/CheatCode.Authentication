using CheatCode.Authentication.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace CheatCode.Authentication.OAuth.Client.Controllers
{
    public class HomeController : Controller
    {
        HttpClient httpClient = new HttpClient();
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var serverRequest = () => { return GetAsync($"{SharedValues.OAuth.ServerAddress}/secret/index"); };
            var serverResponse = await SendMessageWrapper(serverRequest);

            await RefreshToken();

            var apiReqeust = () => { return GetAsync($"{SharedValues.OAuth.ApiAddress}/secret/index"); };
            var apiReponse = await SendMessageWrapper(apiReqeust);

            return View(model: serverResponse.StatusCode.ToString() + "/" + apiReponse.StatusCode.ToString()); 
        }

        async Task<HttpResponseMessage> GetAsync(string url)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Add("Authorization", $"Bearer {accessToken}");

            return await httpClient.SendAsync(requestMessage);
        }

        async Task<HttpResponseMessage> SendMessageWrapper(Func<Task<HttpResponseMessage>> requestFunc)
        {
            var response = await requestFunc();

            // 401응답인 경우 액세스토큰 만료로 판단한다.
            // 클라이언트에서 토큰만료시간을 관리하는 것은 신뢰할 수 없으며
            // 서버 응답을 통해 현재 토큰이 만료되었는지 판단할 수 있다.
            // OAuth공급자마다 별도의 응답을 보낼수 있으니 잘 확인할 것. 
            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await RefreshToken();
                response = await requestFunc();
            }
            return response;
        }

        public async Task RefreshToken()
        {
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            var data = new Dictionary<string, string>()
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = refreshToken,
            };

            var request = new HttpRequestMessage(HttpMethod.Post, SharedValues.OAuth.ServerAddress+"/oauth/token");
            request.Content = new FormUrlEncodedContent(data);

            var basicCredentials = "username:password";
            var encodedCredentials = Encoding.UTF8.GetBytes(basicCredentials);
            var base64Credentials = Convert.ToBase64String(encodedCredentials); ;

            request.Headers.Add("Authorization", $"Basic {base64Credentials}");


            var response = await new HttpClient().SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

            var newAccessToken = responseData["access_token"];
            var newRefreshToken = responseData["refresh_token"];

            // 기본인증 실행 후 현재 컨텍스트에 토큰값을 갱신한다.
            var authResult = await HttpContext.AuthenticateAsync("ClientCookie");

            authResult.Properties.UpdateTokenValue("access_token", newAccessToken);
            authResult.Properties.UpdateTokenValue("refresh_token", newRefreshToken);

            // 갱신된 인증데이터로 다시 로그인 한다
            await HttpContext.SignInAsync("ClientCookie", authResult.Principal, authResult.Properties);
        }


    }
}