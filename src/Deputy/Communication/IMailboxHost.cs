using System;

namespace Deputy.Communication
{
    public interface IMailboxHost
    {
        void Deliver<TMessage>(TMessage message);
        void Deliver<TMessage>(TMessage message, string name);

        IDisposable Subscribe<TMessage>(IObserver<TMessage> observer);
        IDisposable Subscribe<TMessage>(IObserver<TMessage> observer, string name);
    }
}
