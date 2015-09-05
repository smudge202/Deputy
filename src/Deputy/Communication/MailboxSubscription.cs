using System;                   

namespace Deputy.Communication
{
    public class MailboxSubscription<T> : IMailboxSubscription<T>
    {
        private readonly IActor _actor;

        public MailboxSubscription(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            _actor = actor;
        }

        public void OnCompleted()
        {
        }
        public void OnError(Exception error)
        {
        }
        public void OnNext(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _actor.Mailbox.Deliver(value);
        }
    }
}
