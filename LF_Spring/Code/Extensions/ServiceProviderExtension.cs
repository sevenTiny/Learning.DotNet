﻿using SevenTiny.Bantina.Spring.DependencyInjection;
using System.Linq;

namespace SevenTiny.Bantina.Spring
{
    public static class ServiceProviderExtension
    {
        public static TService GetService<TService>(this IServiceProvider serviceProvider) where TService : class
        {
            return serviceProvider.GetService(typeof(TService)) as TService; ;
        }

        /// <summary>
        /// scan and clear abandon objects
        /// </summary>
        /// <param name="serviceProvider"></param>
        internal static void ScanAbandonService(this IServiceProvider serviceProvider)
        {
            var collection = SpringContext.ServiceCollection;
            if (collection != null && collection.Any())
            {
                foreach (var item in collection)
                {
                    if (item.Value.LifeTime == ServiceLifetime.Scoped)
                    {
                        item.Value.ImplementationInstance = null;
                    }
                }
            }
        }
    }
}
