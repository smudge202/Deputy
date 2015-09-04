using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Deputy
{
    public static class ActorSystemGuardian
    {
        private static readonly ConcurrentDictionary<string, IActorSystem> _knownActorSystems = new ConcurrentDictionary<string, IActorSystem>();
        public static IActorSystem CreateActorSystem(IServiceCollection services) => CreateActorSystem(services, "DefaultActorSystem");
        public static IActorSystem CreateActorSystem(IServiceCollection services, string name) => _knownActorSystems.GetOrAdd(name, key => new ActorSystem(services, key));
        public static void Shutdown(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            IActorSystem actorSystem;
            if (!_knownActorSystems.TryRemove(name, out actorSystem))
                throw new InvalidActorSystemException(name);

            actorSystem.Shutdown();
        }
        public static void ShutdownAll()
        {
            var actorSystems = _knownActorSystems.Values.ToArray();
            _knownActorSystems.Clear();

            foreach (var actorSystem in actorSystems)
                actorSystem.Shutdown();
        }
    }
}
