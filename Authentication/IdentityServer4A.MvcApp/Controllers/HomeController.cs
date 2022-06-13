using IdentityServer4A.MvcApp.Data;
using IdentityServer4A.MvcApp.Models;
using IdentityServer4A.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace IdentityServer4A.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext, 
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //# 로그인 시도
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties()
            {
                //# 리디렉트 경로: IdentityServer -> ~/signin-oidc -> ~/Home/Index
                RedirectUri = "/Home/Index"
            }, Constants.OpenIdConnectScheme);
        }

        public IActionResult Logout()
        {
            return SignOut(new AuthenticationProperties()
            {
                //# 리디렉트 경로: IdentityServer -> ~/signout-callback-oidc -> ~/Home/Index
                RedirectUri = "/Home/Index"
            }, Constants.CookiesScheme, Constants.OpenIdConnectScheme);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}