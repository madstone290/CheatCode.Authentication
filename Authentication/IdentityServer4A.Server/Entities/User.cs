namespace IdentityServer4A.Server.Entities
{
    public class User
    {
        public string IdProvider { get; set; }

        public string Sub { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
