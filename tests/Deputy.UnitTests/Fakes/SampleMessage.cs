using System;

namespace Deputy.UnitTests.Fakes
{
    internal class SampleMessage
    {
        internal string Property1 { get; set; }
        internal string Property2 { get; set; }
        internal string Property3 { get; set; }

        internal static SampleMessage Create(string property1 = null, string property2 = null, string property3 = null) => new SampleMessage
        {
            Property1 = property1 ?? Guid.NewGuid().ToString(),
            Property2 = property2 ?? Guid.NewGuid().ToString(),
            Property3 = property3 ?? Guid.NewGuid().ToString()
        };
    }
}
