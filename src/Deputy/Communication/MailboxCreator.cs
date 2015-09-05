using System;

namespace Deputy.Communication
{
    public class MailboxCreator : IMailboxCreator
    {
        private readonly IServiceProvider _services;

        public MailboxCreator(IServiceProvider services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            _services = services;
        }

        public IMailbox<TMessage> Create<TMessage>() => (IMailbox<TMessage>)_services.GetService(typeof(IMailbox<>).MakeGenericType(typeof(TMessage)));
    }
}
