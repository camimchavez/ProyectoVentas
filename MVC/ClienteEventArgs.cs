using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ventas;

namespace MVC
{
    public class ClienteEventArgs : EventArgs
    {
        private Cliente _cliente;

        public ClienteEventArgs(Cliente _cliente)
        {
            this._cliente = _cliente;
        }

        public Cliente Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
    }
}
