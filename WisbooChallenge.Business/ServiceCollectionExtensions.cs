using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WisbooChallenge.Data;

namespace WisbooChallenge.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataServices(configuration);
            
            return services;
        }
    }
}