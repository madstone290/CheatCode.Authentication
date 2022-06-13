using IdentityServer4B.Shared;

namespace IdentityServer4B.ApiOne
{
    public static class ProgramExtensions
    {
        public static void AddCheatCodeAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(Constants.Bearer)
                .AddJwtBearer(Constants.Bearer, options =>
                {
                    options.Authority = Constants.ServerAddress;

                    options.Audience = Constants.ApiOneName;
                });
        }
        

    }
}
