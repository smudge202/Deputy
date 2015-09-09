using Deputy.Communication;
using Microsoft.Framework.DependencyInjection;
using System;

namespace Deputy.Actors
{
    public class ServiceResolvedActorFactory : IActorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceResolvedActorFactory(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));

            _serviceProvider = serviceProvider;
        }

        public IActor Create<T>() where T : IActor
        {
            var actor = _serviceProvider.GetRequiredService<T>();

            actor.MailboxSubscriptionFactory = _serviceProvider.GetRequiredService<IMailboxSubscriptionFactory>();

            var baseActorImplementation = actor as ActorBase;
            if (baseActorImplementation != null)
            {
                baseActorImplementation.SetActorFactory(this);
            }

            return actor;
        }
    }
}
