using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVC
{
    public class ExcepcionEventArgs : EventArgs
    {
        private Exception _excepcion;

        public ExcepcionEventArgs(Exception _excepcion)
        {
            this._excepcion = _excepcion;
        }

        public Exception Excepcion
        {
            get { return _excepcion; }
            set { _excepcion = value; }
        }
    }
}
