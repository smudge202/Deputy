using Deputy.Communication;
using Deputy.UnitTests.Fakes;
using FluentAssertions;
using System;
using TestAttributes;

namespace Deputy.UnitTests.Communication
{
    using Utils;

    public class InMemoryTransientMailboxTests
    {
        internal static InMemoryTransientMailbox<SampleMessage> CreateTarget() => new InMemoryTransientMailbox<SampleMessage>();

        public class Deliver
        {
            [Unit]
            public void GivenANullMessageThrowArgumentNullException()
            {
                var mailbox = CreateTarget();

                Action.Throws<ArgumentNullException>(() => mailbox.Deliver(null));
            }

            [Unit]
            public void GivenAMessageAndNoSubscribersThenMessageCountShouldIncreaseByOne()
            {
                var mailbox = CreateTarget();

                mailbox.Deliver(SampleMessage.Create());

                mailbox.MessageCount.Should().Be(1);
            }

            [Unit]
            public void GivenAMessageAndSubscribersAreSubscribedThenMessageCountShouldNotIncrease()
            {
                var mailbox = CreateTarget();

                var observer = Observer.Create<SampleMessage>();
                using (mailbox.Subscribe(observer))
                    mailbox.Deliver(SampleMessage.Create());

                mailbox.MessageCount.Should().Be(0);
                ((Observer.NullObserver<SampleMessage>)observer).MessagesReceived.Should().Be(1);
            }

            [Unit]
            public void GivenAMessageAndNoSubscribersAtTheTimeOfDeliveryAndSubscriptionIsCreatedThenMessageIsPushedToSubscriber()
            {
                var mailbox = CreateTarget();

                mailbox.Deliver(SampleMessage.Create());

                mailbox.MessageCount.Should().Be(1);

                mailbox.Subscribe(Observer.Create<SampleMessage>()).Dispose();

                mailbox.MessageCount.Should().Be(0);
            }
        }

        public class Subscribe
        {
            [Unit]
            public void GivenANullSubscriberThrowArgumentNullException()
            {
                var mailbox = CreateTarget();

                Action.Throws<ArgumentNullException>(() => mailbox.Subscribe(null));
            }

            [Unit]
            public void GivenANonNullSubscriberReturnNonNullDisposable()
            {
                var mailbox = CreateTarget();

                var subscription = mailbox.Subscribe(Observer.Create<SampleMessage>());

                subscription.Should().NotBeNull();
            }

            [Unit]
            public void GivenANonNullSubscriberThenSubscriptionCountShouldIncreaseByOne()
            {
                var mailbox = CreateTarget();

                var subscription = mailbox.Subscribe(Observer.Create<SampleMessage>());

                mailbox.SubscriberCount.Should().Be(1);

                subscription.Dispose();
            }

            [Unit]
            public void GivenANonNullSubscriberWhenCallingDisposeOnSubscriptionThenSubscriptionCountShouldDecreaseByOne()
            {
                var mailbox = CreateTarget();

                var subscription = mailbox.Subscribe(Observer.Create<SampleMessage>());

                mailbox.SubscriberCount.Should().Be(1);

                subscription.Dispose();

                mailbox.SubscriberCount.Should().Be(0);
            }

            [Unit]
            public void GivenANonNullSubscriberAndPendingMessagesBeforeSubscriptionStartsThenShouldRecieveTheSameAmountOfMessagesAsInMailbox()
            {
                var mailbox = CreateTarget();

                mailbox.Deliver(SampleMessage.Create());
                mailbox.Deliver(SampleMessage.Create());

                var mailboxSize = mailbox.MessageCount;

                var observer = (Observer.NullObserver<SampleMessage>)Observer.Create<SampleMessage>();
                var subscription = mailbox.Subscribe(observer);

                observer.MessagesReceived.Should().Be(mailboxSize);

                subscription.Dispose();
            }
        }
    }
}