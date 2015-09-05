using Deputy.Communication;
using System;
using TestAttributes;

namespace Deputy.UnitTests.Communication
{
    using Fakes;
    using FluentAssertions;
    using Utils;

    public class MailboxSubscriptionTests
    {
        internal static IActor DefaultActor() => new FakeActor { Mailbox = new InMemoryActorMailbox() };
        internal static MailboxSubscription<SampleMessage> CreateTarget(IActor actor = null) => new MailboxSubscription<SampleMessage>(actor);

        public class Constructor
        {
            [Unit]
            public void GivenANullActorThrowArgumentNullException()
            {
                Action.Throws<ArgumentNullException>(() => CreateTarget(actor: null));
            }
        }

        public class OnNext
        {
            [Unit]
            public void GivenANullValueThrowArgumentNullException()
            {
                var actor = DefaultActor();
                var subscription = CreateTarget(actor);

                Action.Throws<ArgumentNullException>(() => subscription.OnNext(null));

                ((InMemoryActorMailbox)actor.Mailbox).MessagesReceived.Should().Be(0);
            }

            [Unit]
            public void GivenAValidMessageThenMessageIsDeliveredToActorsMailbox()
            {
                var actor = DefaultActor();
                var subscription = CreateTarget(actor);
                var message = SampleMessage.Create();

                subscription.OnNext(message);

                ((InMemoryActorMailbox)actor.Mailbox).MessagesReceived.Should().Be(1);
            }
        }
    }
}
