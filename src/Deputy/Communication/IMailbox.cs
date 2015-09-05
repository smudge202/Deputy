using System;

namespace Deputy.Communication
{
    public interface IMailbox
    {
        int SubscriberCount { get; }
        int MessageCount { get; }
    }

    public interface IMailbox<T> : IMailbox, IObservable<T>
    {
        void Deliver(T message);
    }
}
