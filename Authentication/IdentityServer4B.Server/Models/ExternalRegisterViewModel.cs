using System.ComponentModel.DataAnnotations;

namespace IdentityServer4B.Server.Models
{
    public class ExternalRegisterViewModel
    {
        public string UserName { get; set; }
        public string ReturnUrl { get; set; }
    }
}
