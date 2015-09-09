using Deputy.Communication;

namespace Deputy
{
    public interface IActor
    {
        IActorMailbox Mailbox { get; }
        IMailboxSubscriptionFactory MailboxSubscriptionFactory { get; set; }
    }
}
