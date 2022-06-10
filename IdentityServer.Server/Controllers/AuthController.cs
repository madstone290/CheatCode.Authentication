using IdentityModel;
using IdentityServer.Server.Data;
using IdentityServer.Server.Identity;
using IdentityServer.Server.Models;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Transactions;

namespace IdentityServer.Server.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly AppIdentityDbContext _appIdentityDbContext;

        public AuthController(
            SignInManager<ApplicationUser> signInManager,
            ApplicationUserManager userManager,
            IIdentityServerInteractionService interactionService, AppIdentityDbContext appIdentityDbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
            _appIdentityDbContext = appIdentityDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Login([FromQuery] string returnUrl)
        {
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();

            var vm = new LoginViewModel() { ReturnUrl = returnUrl, ExternalProviders = externalProviders };
            return View(model: vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            // 모델 유효성 검사
            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(vm.ReturnUrl);
            }
            else if(result.IsLockedOut)
            {

            }
            return View(model: vm);
        }

        [HttpGet]
        public async Task<IActionResult> Logout([FromQuery] string logoutId)
        {
            // 현재 Identity사용자 로그아웃
            await _signInManager.SignOutAsync();

            // IdentityServer 로그아웃
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
                return RedirectToAction("Index", "Home");

            return Redirect(logoutRequest.PostLogoutRedirectUri); 
        }



        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(model: new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            // 로그인 모델 상태 검증
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = new ApplicationUser(vm.UserName);
            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return Redirect(vm.ReturnUrl);
            }

            return View(model: vm);
        }

        [HttpPost]
        public async Task<IActionResult> ExternalRegister(ExternalRegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            // SignInManager.GetExternalLoginInfoAsync() null 반환 버그
            //var info = await _signInManager.GetExternalLoginInfoAsync();
            //if (info == null)
            //    return RedirectToAction(nameof(Login));
            var authenticateResult = await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (!authenticateResult.Succeeded)
                return RedirectToAction(nameof(Login));
            
            var user = new ApplicationUser(vm.UserName);

            using var transaction = _appIdentityDbContext.Database.BeginTransaction();
            try
            {
                // 사용자가 존재하고 로그인정보가 없으면 로그인정보만 추가
                // 사용자가 없으면 생성하고 로그인정보 추가
                 
                // 신규 사용자 등록
                var createResult = await _userManager.CreateAsync(user);

                if (!createResult.Succeeded)
                    return View(model: vm);

                var provider = authenticateResult.Properties.Items["provider"];
                var providerKey = authenticateResult.Principal.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier || x.Type == JwtClaimTypes.Subject)
                    .Value;
                var username = authenticateResult.Principal.Claims
                  .FirstOrDefault(x => x.Type == ClaimTypes.Name)
                  ?.Value ?? "";

                var loginInfo = new UserLoginInfo(provider, providerKey, username);

                // 사용자에 외부 로그인 정보 추가
                var loginResult = await _userManager.AddLoginAsync(user, loginInfo);
                if (!loginResult.Succeeded)
                    return View(model: vm);

                // 사용자 로그인 진행
                await _signInManager.SignInAsync(user, isPersistent: false);

                transaction.Commit();
                
            }
            catch
            {
                transaction.Rollback();
            }
          

        
            return Redirect(vm.ReturnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> ExternalLogin([FromForm]string provider, [FromQuery] string returnUrl)
        {
            // redirect to external provider login page

            var redirectUri = Url.Action(nameof(ExternalLoginCallback), "Auth", new { returnUrl});

            // LoginProvider: {provider} 
            //var properites = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUri);
            var properites = new AuthenticationProperties
            {
                RedirectUri = redirectUri,
                Items =
                {
                    { "returnUrl", returnUrl },
                    { "provider", provider },
                }
            };
            return Challenge(properites, provider);
        }

        public async Task<IActionResult> ExternalLoginCallback([FromQuery] string returnUrl)
        {
            // SignInManger.GetExternalLoginInfoAsync() 사용시 null 반환 버그 있음. 2022-06-09
            // HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme)로 대체
            //var info = await _signInManager.GetExternalLoginInfoAsync();
            //if (info == null)
            //    RedirectToAction(nameof(Login));

            var authenticateResult = await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (!authenticateResult.Succeeded)
                return RedirectToAction(nameof(Login));

            var provider = authenticateResult.Properties.Items["provider"];
            var providerKey = authenticateResult.Principal.Claims
                .FirstOrDefault(x=> x.Type == ClaimTypes.NameIdentifier || x.Type == JwtClaimTypes.Subject)
                .Value;

            var applicationUser = await _userManager.FindByLoginAsync(provider, providerKey);

            if(applicationUser == null)
            {
                return View("ExternalRegister",
                new ExternalRegisterViewModel()
                {
                    UserName = authenticateResult.Principal.FindFirst(ClaimTypes.Name)?.Value
                });
            }

            if(applicationUser.Deleted)
            {
                return Ok("탈퇴한 계정입니다");
            }

            var loginResult = await _signInManager.ExternalLoginSignInAsync(provider, providerKey, false);

            if (!loginResult.Succeeded)
                return View("ExternalRegister",
                 new ExternalRegisterViewModel()
                 {
                     UserName = authenticateResult.Principal.FindFirst(ClaimTypes.Name)?.Value
                 });
            
            
            return Redirect(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUserAsync([FromQuery] string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            await _userManager.DeleteAsync(user);

            return Ok();
        }

    }
}