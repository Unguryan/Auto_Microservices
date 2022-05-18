using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;
using UI.Interfaces;

namespace UI.Extra
{
    public class WpfDispatch : IDispatch
    {
        private readonly Dispatcher _dispatcher;

        public WpfDispatch(Dispatcher dispatcher) =>
            _dispatcher = dispatcher;

        public bool CheckAccess() => _dispatcher.CheckAccess();

        public void Invoke(Action action) => _dispatcher.Invoke(action);
    }
}
