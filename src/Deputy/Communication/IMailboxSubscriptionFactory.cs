using System;

namespace Deputy.Communication
{
    public interface IMailboxSubscriptionFactory
    {
        IDisposable Create<TActor, TMessage>(TActor actor) where TActor : IActor;
        IDisposable Create<TActor, TMessage>(TActor actor, string name) where TActor : IActor;
    }
}
