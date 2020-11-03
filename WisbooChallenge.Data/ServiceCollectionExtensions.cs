using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WisbooChallenge.Data.Core;
using WisbooChallenge.Data.Interfaces;
using WisbooChallenge.Data.Classes;

namespace WisbooChallenge.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(configuration.GetSection("DefaultConnection"));

            services.AddScoped<IDBManager, DBManager>();
            
            services.AddScoped<IVideoMediaData, VideoMediaData>();
            services.AddScoped<IVideoCommentData, VideoCommentData>();

            return services;
        }
    }
}