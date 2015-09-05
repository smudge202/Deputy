using Deputy.Communication;

namespace Deputy.UnitTests.Fakes
{
    public class InMemoryMailboxCreator : IMailboxCreator
    {
        public IMailbox<TMessage> Create<TMessage>() => new InMemoryTransientMailbox<TMessage>();
    }
}
