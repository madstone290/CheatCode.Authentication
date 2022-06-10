using CheatCode.Authentication.IdServer4.MvcApp.Data;
using CheatCode.Authentication.IdServer4.MvcApp.Models;
using CheatCode.Authentication.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CheatCode.Authentication.IdServer4.MvcApp.Controllers
{
    public class MyHomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public MyHomeController(ILogger<HomeController> logger, AppDbContext appDbContext,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(Index));

            return View();
        }

        [HttpGet]
        public IActionResult WrongInfo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromForm] string userName, [FromForm] string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(WrongInfo));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromForm] string userName, [FromForm] string password)
        {
            var user = new IdentityUser
            {
                UserName = userName,
                Email = string.Empty,
            };
            var result = await _userManager.CreateAsync(user, password);
            await _userManager.AddClaimAsync(user, new Claim("Age", "10"));
            if (result.Succeeded)
            {
                // sign user
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(WrongInfo));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy ="Old")]
        public IActionResult Secret()
        {
            return View();
        }

        //# 임시로 신원을 만들어 로그인
        public async Task<IActionResult> AuthenticateWithTempUser()
        {
            var grandmaClaims = new Claim[]
            {
                new Claim(ClaimTypes.Name, "bob"),
                new Claim(ClaimTypes.Email, "bob@mail.com"),
                new Claim("Grandma.Says", "Very nice boy"),
                new Claim("Age", "19"),
            };

            var licenseClaims = new Claim[]
            {
                new Claim(ClaimTypes.Name, "Bob K Foo"),
                new Claim("DrivingLicense", "A+"),
            };

            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "GrandmaIdentity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity, licenseIdentity });

            await HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction(nameof(Index));
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

