using System;

namespace Deputy.Utils
{
    internal static class Disposable
    {
        internal static IDisposable Create(Action finalizeAction = null) => new FinalizedDisposable(finalizeAction);

        private class FinalizedDisposable : IDisposable
        {
            private readonly Action _finalizeAction;

            internal FinalizedDisposable(Action action)
            {
                _finalizeAction = action;
            }

            public void Dispose()
            {
                if (_finalizeAction != null)
                    _finalizeAction();
            }
        }
    }
}
