using CardOrgAPI.Common.Interfaces;
using CardOrgAPI.Identity.Helpers;
using CardOrgAPI.Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CardOrgAPI.Identity
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AuthSettings>(config.GetSection(nameof(AuthSettings)));
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}