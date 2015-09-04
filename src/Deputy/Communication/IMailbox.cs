using System;

namespace Deputy.Communication
{
    /// <summary>
    ///     Holds all messages for a specific actor. You can subscribe to the mailbox to recieve a copy of the message.
    /// </summary>
    public interface IMailbox<T> : IObservable<T>
    {
        int SubscriberCount { get; }
        int MessageCount { get; }

        void Deliver(T message);
    }
}
