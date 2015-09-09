using Deputy.Communication;

namespace Deputy.UnitTests.Fakes
{
    internal sealed class FakeActor : IActor
    {
        public IActorMailbox Mailbox { get; set; }
        public IMailboxSubscriptionFactory MailboxSubscriptionFactory { get; set; }
    }
}
