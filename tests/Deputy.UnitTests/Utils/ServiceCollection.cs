using Microsoft.Framework.DependencyInjection;

namespace Deputy.UnitTests.Utils
{
    internal static class ServiceCollection
    {
        internal static IServiceCollection Create() => new Microsoft.Framework.DependencyInjection.ServiceCollection();
    }
}
