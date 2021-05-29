using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ventas;

namespace MVC
{
    public class ModeloEventArgs : EventArgs
    {
        private string _idCliente;
        private Cliente _cliente;

        public ModeloEventArgs(string _idCliente)
        {
            this._idCliente = _idCliente;
        }

        public ModeloEventArgs(Cliente _cliente)
        {
            this._cliente = _cliente;
        }

        public string IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        public Cliente Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
    }
}
