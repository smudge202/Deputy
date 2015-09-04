using FluentAssertions;
using System;

namespace Deputy.UnitTests.Utils
{
    internal static class Action
    {
        internal static void Throws<TException>(System.Action act) where TException : Exception
        {
            act.ShouldThrow<TException>();
        }

        internal static void DoesNotThrow(System.Action act)
        {
            act.ShouldNotThrow();
        }
        internal static TResult DoesNotThrow<TResult>(Func<TResult> act)
        {
            TResult result = default(TResult);
            System.Action dummyAction = () => result = act();

            dummyAction.ShouldNotThrow();

            return result;
        }
    }
}
