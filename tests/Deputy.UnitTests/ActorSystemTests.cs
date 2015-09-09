using FluentAssertions;
using Microsoft.Framework.DependencyInjection;
using System;
using TestAttributes;

namespace Deputy.UnitTests
{
    public class ActorSystemTests
    {
        internal static ActorSystem CreateTarget(IServiceProvider services = null, string name = null) => new ActorSystem(services, name);

        public class Constructor
        {
            [Unit]
            public void GivenANullServicesCollectionThrowArgumentNullException()
            {
                Action act = () => CreateTarget(name: "TestActorSystem");

                act.ShouldThrow<ArgumentNullException>();
            }

            [Unit]
            [WithValues(null)]
            [WithValues("")]
            [WithValues(" ")]
            public void GivenAnInvalidNameThenThrowArgumentNullException(string name)
            {
                Action act = () => CreateTarget(services: Utils.ServiceCollection.Create().BuildServiceProvider(), name: name);

                act.ShouldThrow<ArgumentNullException>();
            }

            [Unit]
            public void GivenAValidNameThenTheNamePropertyShouldBeSetCorrectly()
            {
                const string actorSystemName = "DefaultActorSystem";
                var actorSystem = CreateTarget(services: Utils.ServiceCollection.Create().BuildServiceProvider(), name: actorSystemName);

                actorSystem.Name.Should().Be(actorSystemName);

                actorSystem.Shutdown();
            }
        }

        public class Shutdown
        {

        }
    }
}
