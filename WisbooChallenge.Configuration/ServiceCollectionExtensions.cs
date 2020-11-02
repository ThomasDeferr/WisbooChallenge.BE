using System;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using WisbooChallenge.Configuration.Profiles;

namespace WisbooChallenge.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProfiles(this IServiceCollection services, params Type[] profileAssemblyMarkerTypes)
        {
            services.AddAutoMapper(c => 
            {
                c.AddProfile<EntityProfile>();

                c.AddProfile<VideoMediaProfile>();
                c.AddProfile<VideoCommentProfile>();
            }, profileAssemblyMarkerTypes);

            return services;
        }
    }
}