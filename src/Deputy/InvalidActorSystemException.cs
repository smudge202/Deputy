using System;

namespace Deputy
{
    public class InvalidActorSystemException : Exception
    {
        public InvalidActorSystemException() { }
        public InvalidActorSystemException(string name) : base($"Invalid reference to Actor System '{name}'.") { }
        public InvalidActorSystemException(string message, Exception inner) : base(message, inner) { }
    }
}
