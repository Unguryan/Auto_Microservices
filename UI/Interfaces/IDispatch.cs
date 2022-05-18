using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Interfaces
{
    public interface IDispatch
    {
        bool CheckAccess();
        void Invoke(Action action);
    }
}
