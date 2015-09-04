using System;

namespace Deputy.UnitTests.Utils
{
    internal static class Observer
    {
        internal static IObserver<T> Create<T>() => new NullObserver<T>();
        
        internal class NullObserver<T> : IObserver<T>
        {
            internal int MessagesReceived { get; private set; } = 0;

            public void OnCompleted()
            {
            }

            public void OnError(Exception error)
            {
            }

            public void OnNext(T value)
            {
                MessagesReceived++;
            }
        }
    }
}
