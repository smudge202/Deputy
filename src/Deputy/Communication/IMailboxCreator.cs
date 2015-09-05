namespace Deputy.Communication
{
    public interface IMailboxCreator
    {
        IMailbox<TMessage> Create<TMessage>();
    }
}
