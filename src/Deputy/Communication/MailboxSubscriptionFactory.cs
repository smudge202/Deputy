using System;

namespace Deputy.Communication
{
    public class MailboxSubscriptionFactory : IMailboxSubscriptionFactory
    {
        private readonly IMailboxHost _mailboxHost;

        public MailboxSubscriptionFactory(IMailboxHost mailboxHost)
        {
            if (mailboxHost == null)
                throw new ArgumentNullException(nameof(mailboxHost));

            _mailboxHost = mailboxHost;
        }

        public IDisposable Create<TActor, TMessage>(TActor actor) where TActor : IActor
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            return _mailboxHost.Subscribe(new MailboxSubscription<TMessage>(actor));
        }

        public IDisposable Create<TActor, TMessage>(TActor actor, string name) where TActor : IActor
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(actor));

            return _mailboxHost.Subscribe(new MailboxSubscription<TMessage>(actor), name);
        }
    }
}