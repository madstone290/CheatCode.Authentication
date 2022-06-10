using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Server.Models
{
    public class ExternalRegisterViewModel
    {
        public string UserName { get; set; }
        public string ReturnUrl { get; set; }
    }
}
