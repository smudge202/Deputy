using Deputy.Actors;
using Deputy.Communication;
using Microsoft.Framework.DependencyInjection;

namespace Deputy.UnitTests.Utils
{
    internal static class ServiceCollection
    {
        internal static IServiceCollection Create()
        {
            var serviceCollection = new Microsoft.Framework.DependencyInjection.ServiceCollection();

            serviceCollection.AddTransient<IMailboxCreator, MailboxCreator>();
            serviceCollection.AddTransient<IMailboxHost, MailboxHost>();
            serviceCollection.AddTransient<IMailboxSubscriptionFactory, MailboxSubscriptionFactory>();
            serviceCollection.AddTransient<IActorFactory, ServiceResolvedActorFactory>();

            return serviceCollection;
        }
    }
}
