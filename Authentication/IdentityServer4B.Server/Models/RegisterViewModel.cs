using System.ComponentModel.DataAnnotations;

namespace IdentityServer4B.Server.Models
{
    public class RegisterViewModel
    {
        public string ReturnUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
