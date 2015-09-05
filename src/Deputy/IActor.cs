using Deputy.Communication;

namespace Deputy
{
    public interface IActor
    {
        IActorMailbox Mailbox { get; }
    }
}
