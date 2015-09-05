using System;

namespace Deputy.UnitTests.Fakes
{
    internal sealed class NullServiceProvider : IServiceProvider
    {
        public int CallCount { get; private set; }

        public object GetService(Type serviceType)
        {
            CallCount++;

            return null;
        }
    }
}
