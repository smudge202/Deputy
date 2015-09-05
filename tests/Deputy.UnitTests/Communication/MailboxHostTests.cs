using Deputy.Communication;
using Deputy.UnitTests.Fakes;
using System;
using TestAttributes;

namespace Deputy.UnitTests.Communication
{
    using FluentAssertions;
    using Utils;

    public class MailboxHostTests
    {
        internal static IMailboxCreator DefaultMailboxCreator() => new InMemoryMailboxCreator();
        internal static MailboxHost CreateTarget(IMailboxCreator mailboxCreator = null) => new MailboxHost(mailboxCreator);

        public class Constructor
        {
            [Unit]
            public void GivenANullMailboxCreatorThrowArgumentNullException()
            {
                Action.Throws<ArgumentNullException>(() => CreateTarget(mailboxCreator: null));
            }
        }

        public class Deliver
        {
            [Unit]
            public void GivenANullMessageThrowArgumentNullException()
            {
                var host = CreateTarget(mailboxCreator: DefaultMailboxCreator());

                Action.Throws<ArgumentNullException>(() => host.Deliver<SampleMessage>(null));
            }

            [Unit]
            public void GivenANullMessageAndNameSpecifiedThrowArgumentNullException()
            {
                var host = CreateTarget(mailboxCreator: DefaultMailboxCreator());

                Action.Throws<ArgumentNullException>(() => host.Deliver<SampleMessage>(null, "test"));
            }

            [Unit]
            [WithValues(null)]
            [WithValues("")]
            [WithValues(" ")]
            public void GivenAMessageButAnInvalidNameThrowArgumentNullException(string testCase)
            {
                var host = CreateTarget(mailboxCreator: DefaultMailboxCreator());

                Action.Throws<ArgumentNullException>(() => host.Deliver(SampleMessage.Create(), testCase));
            }

            [Unit]
            public void GivenAMessageThenCanDeliverWithoutError()
            {
                var host = CreateTarget(mailboxCreator: DefaultMailboxCreator());

                Action.DoesNotThrow(() => host.Deliver(SampleMessage.Create()));
            }

            [Unit]
            public void GivenAMessageAndSpecifiedNameThenCanDeliverWithoutError()
            {
                var host = CreateTarget(mailboxCreator: DefaultMailboxCreator());

                Action.DoesNotThrow(() => host.Deliver(SampleMessage.Create(), "test"));
            }
        }

        public class Subscribe
        {
            [Unit]
            public void GivenANullObserverThrowArgumentNullException()
            {
                var host = CreateTarget(mailboxCreator: DefaultMailboxCreator());

                Action.Throws<ArgumentNullException>(() => host.Subscribe<SampleMessage>(null));
            }

            [Unit]
            public void GivenANullObserverAndNameIsSpecifiedThrowArgumentNullException()
            {
                var host = CreateTarget(mailboxCreator: DefaultMailboxCreator());

                Action.Throws<ArgumentNullException>(() => host.Subscribe<SampleMessage>(null, "test"));
            }

            [Unit]
            [WithValues(null)]
            [WithValues("")]
            [WithValues(" ")]
            public void GivenAValidObserverAndInvalidNameThrowArgumentNullException(string testCase)
            {
                var host = CreateTarget(mailboxCreator: DefaultMailboxCreator());

                Action.Throws<ArgumentNullException>(() => host.Subscribe(Observer.Create<SampleMessage>(), testCase));
            }

            [Unit]
            public void GivenAValidObserverReturnsValidSubscription()
            {
                var host = CreateTarget(mailboxCreator: DefaultMailboxCreator());

                var subscription = host.Subscribe(Observer.Create<SampleMessage>());

                subscription.Should().NotBeNull();

                subscription.Dispose();
            }

            [Unit]
            public void GivenAValidObserverWithANameReturnsValidSubscription()
            {
                var host = CreateTarget(mailboxCreator: DefaultMailboxCreator());

                var subscription = host.Subscribe(Observer.Create<SampleMessage>(), "test");

                subscription.Should().NotBeNull();

                subscription.Dispose();
            }
        }
    }
}
