using CheatCode.Authentication.Shared;

namespace IdentityServer.ApiOne
{
    public static class ProgramExtensions
    {
        public static void AddCheatCodeAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(SharedValues.IdentityServer.Bearer)
                .AddJwtBearer(SharedValues.IdentityServer.Bearer, options =>
                {
                    options.Authority = SharedValues.IdentityServer.ServerAddress;

                    options.Audience = SharedValues.IdentityServer.ApiOneName;
                });
        }
        

    }
}
