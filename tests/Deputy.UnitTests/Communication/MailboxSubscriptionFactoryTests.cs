using Deputy.Communication;
using System;
using TestAttributes;

namespace Deputy.UnitTests.Communication
{
    using Fakes;
    using FluentAssertions;
    using Utils;

    public class MailboxSubscriptionFactoryTests
    {
        internal static MailboxHost DefaultMailboxHost() => new MailboxHost(new InMemoryMailboxCreator());
        internal static MailboxSubscriptionFactory CreateTarget(IMailboxHost mailboxHost = null) => new MailboxSubscriptionFactory(mailboxHost);

        public class Constructor
        {
            [Unit]
            public void GivenANullMailboxHostThrowArgumentNullException()
            {
                Action.Throws<ArgumentNullException>(() => CreateTarget(mailboxHost: null));
            }
        }

        public class Create
        {
            [Unit]
            public void GivenANullActorThrowArgumentNullException()
            {
                var factory = CreateTarget(mailboxHost: DefaultMailboxHost());

                Action.Throws<ArgumentNullException>(() => factory.Create<FakeActor, SampleMessage>(null));
            }

            [Unit]
            [WithValues(null)]
            [WithValues("")]
            [WithValues(" ")]
            public void GivenAnActorWithNoNameThrowArgumentNullException(string testCase)
            {
                var factory = CreateTarget(mailboxHost: DefaultMailboxHost());

                Action.Throws<ArgumentNullException>(() => factory.Create<FakeActor, SampleMessage>(new FakeActor(), testCase));
            }

            [Unit]
            public void GivenAnActorReturnsNonNullSubscription()
            {
                var factory = CreateTarget(mailboxHost: DefaultMailboxHost());

                var subscription = factory.Create<FakeActor, SampleMessage>(new FakeActor());

                subscription.Should().NotBeNull();

                subscription.Dispose();
            }

            [Unit]
            public void GivenAnActorWithNameReturnsNonNullSubscription()
            {
                var factory = CreateTarget(mailboxHost: DefaultMailboxHost());

                var subscription = factory.Create<FakeActor, SampleMessage>(new FakeActor(), "test");

                subscription.Should().NotBeNull();

                subscription.Dispose();
            }
        }
    }
}
