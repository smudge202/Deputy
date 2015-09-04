using FluentAssertions;
using Microsoft.Framework.DependencyInjection;
using System;
using TestAttributes;

namespace Deputy.UnitTests
{
    using Utils;

    public class ActorSystemGuardianTests
    {
        internal static IActorSystem CreateTarget(IServiceCollection services = null, string name = null) => ActorSystemGuardian.CreateActorSystem(services, name);

        public class CreateActorSystem
        {
            [Unit]
            public void GivenANullServiceCollectionThrowArgumentNullCollection()
            {
                Action.Throws<ArgumentNullException>(() => CreateTarget(name: "TestSystem"));
            }

            [Unit]
            [WithValues(null)]
            [WithValues("")]
            [WithValues(" ")]
            public void GivenAnInvalidNameThrowArgumentNullException(string testCase)
            {
                Action.Throws<ArgumentNullException>(() => CreateTarget(services: ServiceCollection.Create(), name: testCase));
            }

            [Unit]
            public void GivenNoNameIsSuppliedThenUsesDefaultName()
            {
                var actorSystem = ActorSystemGuardian.CreateActorSystem(ServiceCollection.Create());

                actorSystem.Name.Should().Be("DefaultActorSystem");

                actorSystem.Shutdown();
            }
        }

        public class Shutdown
        {

        }

        public class ShutdownAll
        {

        }
    }
}
