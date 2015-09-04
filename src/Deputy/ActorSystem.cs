using Microsoft.Framework.DependencyInjection;
using System;

namespace Deputy
{
    public sealed class ActorSystem : IActorSystem
    {
        private bool _disposed = false;
        public string Name { get; private set; }

        internal ActorSystem(IServiceCollection services, string name)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public void Shutdown()
        {
        }
        public void Dispose()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ActorSystem));

            Shutdown();
            _disposed = true;
        }
    }
}
