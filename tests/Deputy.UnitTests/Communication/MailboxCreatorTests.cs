using System;
using FluentAssertions;
using Deputy.Communication;
using TestAttributes;

namespace Deputy.UnitTests.Communication
{
    using Fakes;
    using Utils;

    public class MailboxCreatorTests
    {
        internal static IMailboxCreator CreateTarget(IServiceProvider serviceProvider = null) => new MailboxCreator(serviceProvider);

        public class Constructor
        {
            [Unit]
            public void GivenANullServiceProviderThenThrowArgumentNullException()
            {
                Action.Throws<ArgumentNullException>(() => CreateTarget(serviceProvider: null));
            }
        }

        public class Create
        {
            [Unit]
            public void GivenAMessageTypeThenServiceProviderIsCalledToCreateInstance()
            {
                var serviceProvider = new NullServiceProvider();

                CreateTarget(serviceProvider: serviceProvider).Create<SampleMessage>();

                serviceProvider.CallCount.Should().Be(1);
            }
        }
    }
}