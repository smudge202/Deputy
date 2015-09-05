using Deputy.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Deputy.Communication
{
    internal class InMemoryTransientMailbox<T> : IMailbox<T>
    {
        private readonly List<IObserver<T>> _mailboxSubscribers = new List<IObserver<T>>();
        private readonly ConcurrentQueue<T> _mailbox = new ConcurrentQueue<T>();

        public int SubscriberCount => _mailboxSubscribers.Count;
        public int MessageCount => _mailbox.Count;

        public void Deliver(T message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (_mailboxSubscribers.Count > 0)
                lock (_mailboxSubscribers)
                    foreach (var subscriber in _mailboxSubscribers)
                        subscriber.OnNext(message);
            else
                _mailbox.Enqueue(message);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));

            lock (_mailboxSubscribers)
            {
                _mailboxSubscribers.Add(observer);

                if(_mailboxSubscribers.Count == 1)
                {
                    T message;
                    while (MessageCount > 0 && _mailbox.TryDequeue(out message))
                        observer.OnNext(message);
                }
            }

            return Disposable.Create(() => {

                lock(_mailboxSubscribers)
                    _mailboxSubscribers.Remove(observer);

                observer.OnCompleted();
            });
        }
    }
}
