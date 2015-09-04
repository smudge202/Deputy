using Deputy.Communication;
using Deputy.DependencyInjection;
using Deputy.UnitTests.Fakes;
using FluentAssertions;
using Microsoft.Framework.DependencyInjection;
using System;
using TestAttributes;

namespace Deputy.UnitTests.DependencyInjection
{
    using Utils;

    public class ServiceExtensionTests
    {
        public class AddDeputy
        {
            [Unit]
            public void GivenANullServiceCollectionThrowArgumentNullException()
            {
                Action.Throws<ArgumentNullException>(() => ServiceExtensions.AddDeputy(null));
            }

            [Unit]
            public void GivenNoConfigurationActionThenDefaultsAreAddedToTheServiceCollection()
            {
                var services = ServiceCollection.Create();

                services = services.AddDeputy();

                var serviceProvider = services.BuildServiceProvider();

                Action.DoesNotThrow(() =>  serviceProvider.GetRequiredService<IMailbox<SampleMessage>>()).Should().NotBeNull();
            }
        }
    }
}
