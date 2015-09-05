using System;
using System.Collections.Concurrent;

namespace Deputy.Communication
{
    public class MailboxHost : IMailboxHost
    {
        private readonly ConcurrentDictionary<Type, IMailbox> _mailboxes = new ConcurrentDictionary<Type, IMailbox>();
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<Type, IMailbox>> _namedMailboxes = new ConcurrentDictionary<string, ConcurrentDictionary<Type, IMailbox>>();
        private readonly IMailboxCreator _mailboxCreator;

        public MailboxHost(IMailboxCreator mailboxCreator)
        {
            if (mailboxCreator == null)
                throw new ArgumentNullException(nameof(mailboxCreator));

            _mailboxCreator = mailboxCreator;
        }

        public void Deliver<TMessage>(TMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            FindMailbox<TMessage>().Deliver(message);
        }
        public void Deliver<TMessage>(TMessage message, string name)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            FindMailbox<TMessage>(name).Deliver(message);
        }

        public IDisposable Subscribe<TMessage>(IObserver<TMessage> observer)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));

            return FindMailbox<TMessage>().Subscribe(observer);
        }
        public IDisposable Subscribe<TMessage>(IObserver<TMessage> observer, string name)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return FindMailbox<TMessage>(name).Subscribe(observer);
        }

        private IMailbox<TMessage> FindMailbox<TMessage>() => (IMailbox<TMessage>)_mailboxes.GetOrAdd(typeof(TMessage), messageType => _mailboxCreator.Create<TMessage>());
        private IMailbox<TMessage> FindMailbox<TMessage>(string name)
        {
            var namedMailbox = _namedMailboxes.GetOrAdd(name, key => new ConcurrentDictionary<Type, IMailbox>());

            return (IMailbox<TMessage>)_mailboxes.GetOrAdd(typeof(TMessage), messageType => _mailboxCreator.Create<TMessage>());
        }
    }
}
