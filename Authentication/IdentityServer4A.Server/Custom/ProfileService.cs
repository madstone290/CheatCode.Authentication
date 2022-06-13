using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServer4A.Server.Custom
{
    /// <summary>
    /// 스코프 요청에 관계없이 클레임을 추가한다.
    /// </summary>
    public class ProfileService : IProfileService
    {
        //private readonly UserManager<PafUser> _userManager;

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var claims = new Claim[]
            {
                new Claim("UserEmail", "abc@gmail.com"),
                new Claim("UserId", "1234")
            };
            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }
    }
}
