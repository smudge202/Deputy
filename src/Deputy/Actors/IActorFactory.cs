namespace Deputy.Actors
{
    public interface IActorFactory
    {
        IActor Create<T>() where T : IActor;
    }
}
