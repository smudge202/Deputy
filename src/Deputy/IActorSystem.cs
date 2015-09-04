using System;

namespace Deputy
{
    public interface IActorSystem : IDisposable
    {
        string Name { get; }

        void Shutdown();
    }
}
