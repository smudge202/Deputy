using Deputy.Actors;
using Deputy.Communication;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Deputy
{
    public sealed class ActorSystem : IActorSystem
    {
        private bool _disposed = false;
        private readonly List<IActor> _topLevelActors = new List<IActor>();
        private readonly IActorFactory _actorFactory;
        private IServiceProvider _serviceProvider;
        public string Name { get; private set; }
        public IMailboxHost MailboxHost { get; private set; }
        public IMailboxSubscriptionFactory MailboxSubscriptionFactory { get; private set; }

        internal ActorSystem(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));

            _serviceProvider = serviceProvider;
            _actorFactory = _serviceProvider.GetRequiredService<IActorFactory>();
            MailboxHost = _serviceProvider.GetRequiredService<IMailboxHost>();
            MailboxSubscriptionFactory = _serviceProvider.GetRequiredService<IMailboxSubscriptionFactory>();
        }

        internal ActorSystem(IServiceProvider serviceProvider, string name) : this(serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public IActor CreateActor<T>() where T : IActor
        {
            var actor = _actorFactory.Create<T>();

            _topLevelActors.Add(actor);

            return actor;
        }
        public void Deliver<T>(T message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            MailboxHost.Deliver(message);
        }
        public void Deliver<T>(T message, string mailboxName)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (string.IsNullOrWhiteSpace(mailboxName))
                throw new ArgumentNullException(nameof(mailboxName));

            MailboxHost.Deliver(message, mailboxName);
        }
        public void Shutdown()
        {
        }
        public void Dispose()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ActorSystem));

            Shutdown();
            _disposed = true;
        }
    }
}
