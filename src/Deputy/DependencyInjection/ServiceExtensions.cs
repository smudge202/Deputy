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
            // TODO :: Investigate why TryAddTransient throws a missing method exception: http://puu.sh/k06BU/aa1f262461.png
            services.AddTransient(typeof(IMailbox<>), typeof(InMemoryTransientMailbox<>));
            services.AddTransient<IMailboxCreator, MailboxCreator>();
            services.AddTransient<IMailboxHost, MailboxHost>();
            services.AddTransient<IMailboxSubscriptionFactory, MailboxSubscriptionFactory>();

            return services;
        }
    }
}
