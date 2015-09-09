using System;
using Deputy.Communication;
using System.Collections.Generic;

namespace Deputy.Actors
{
    public abstract class ActorBase : IActor, IDisposable
    {
        private List<IActor> _childActors = new List<IActor>();
        private List<IDisposable> _subscriptions = new List<IDisposable>();
        private IActorFactory _actorFactory;

        internal IActorFactory ActorFactory { get; set; }
        public IActorMailbox Mailbox { get; private set; }
        public IMailboxSubscriptionFactory MailboxSubscriptionFactory { get; set; }

        protected void Recieve<T>()
        {
            _subscriptions.Add(MailboxSubscriptionFactory.Create<ActorBase, T>(this));
        }
        protected void Recieve<T>(string mailboxName)
        {
            if (string.IsNullOrWhiteSpace(mailboxName))
                throw new ArgumentNullException(nameof(mailboxName));

            _subscriptions.Add(MailboxSubscriptionFactory.Create<ActorBase, T>(this, mailboxName));
        }
        protected IActor CreateChildActor<T>() where T : IActor
        {
            var actor = _actorFactory.Create<T>();

            _childActors.Add(actor);

            return actor;
        }

        internal void SetActorFactory(IActorFactory actorFactory)
        {
            if (actorFactory == null)
                throw new ArgumentNullException(nameof(actorFactory));

            _actorFactory = actorFactory;
        }

        public void Dispose()
        {
            if (_subscriptions != null)
            {
                foreach (var subscription in _subscriptions)
                    subscription.Dispose();

                _subscriptions = null;
            }
        }
    }
}
