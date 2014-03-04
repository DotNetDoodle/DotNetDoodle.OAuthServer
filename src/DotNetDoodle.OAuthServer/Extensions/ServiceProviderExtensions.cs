using System;

namespace DotNetDoodle.OAuthServer
{
    public static class ServiceProviderExtensions
    {
        public static TService GetService<TService>(this IServiceProvider provider) where TService : class
        {
            return provider.GetService(typeof(TService)) as TService;
        }
    }
}