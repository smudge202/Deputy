using Deputy.Communication;
using Microsoft.Framework.DependencyInjection;
using System;

namespace Deputy.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDeputy(this IServiceCollection services, Action<IActorSystemBuilder> configuration = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration != null)
            {
                // TODO :: Implement actor system builder
            }

            return services.AddDefaultServices();
        }

        internal static IServiceCollection AddDefaultServices(this IServiceCollection services)
        {
            services.TryAddTransient(typeof(IMailbox<>), typeof(InMemoryTransientMailbox<>));

            return services;
        }
    }
}
