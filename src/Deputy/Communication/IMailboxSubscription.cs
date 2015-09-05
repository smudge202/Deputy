using System;

namespace Deputy.Communication
{
    public interface IMailboxSubscription<T> : IObserver<T>
    {
    }
}
