using Deputy.Communication;
using System;

namespace Deputy
{
    public interface IActorSystem : IDisposable
    {
        IMailboxHost MailboxHost { get; }
        IMailboxSubscriptionFactory MailboxSubscriptionFactory { get; }
        string Name { get; }

        IActor CreateActor<T>() where T : IActor;
        void Deliver<T>(T message);
        void Deliver<T>(T message, string mailboxName);
        void Shutdown();
    }
}
