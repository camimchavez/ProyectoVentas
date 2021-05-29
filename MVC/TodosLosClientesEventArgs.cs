using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ventas;

namespace MVC
{
    public class TodosLosClientesEventArgs : EventArgs
    {
        private Cliente[] _todosLosClientes;

        public TodosLosClientesEventArgs(Cliente[] _todosLosClientes)
        {
            this._todosLosClientes = _todosLosClientes;
        }

        public TodosLosClientesEventArgs()
        {
            this._todosLosClientes = null;
        }


        public Cliente[] TodosLosClientes
        {
            get { return _todosLosClientes; }
            set { _todosLosClientes = value; }
        }
    }
}
