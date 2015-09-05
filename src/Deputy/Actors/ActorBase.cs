using System;
using Deputy.Communication;

namespace Deputy.Actors
{
    public class ActorBase : IActor
    {
        public IActorMailbox Mailbox
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
