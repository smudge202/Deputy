namespace Deputy.Communication
{
    public interface IActorMailbox
    {
        void Deliver(object message);
    }
}
