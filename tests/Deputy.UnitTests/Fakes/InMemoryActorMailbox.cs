using Deputy.Communication;
using System.Collections.Generic;

namespace Deputy.UnitTests.Fakes
{
    internal sealed class InMemoryActorMailbox : IActorMailbox
    {
        public List<object> Messages { get; } = new List<object>();
        public int MessagesReceived => Messages.Count;

        public void Deliver(object message)
        {
            Messages.Add(message);
        }
    }
}
