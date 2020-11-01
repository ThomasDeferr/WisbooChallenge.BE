using WisbooChallenge.Data.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WisbooChallenge.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(configuration.GetSection("DefaultConnection"));

            services.AddScoped<IDBManager, DBManager>();
            
            return services;
        }
    }
}