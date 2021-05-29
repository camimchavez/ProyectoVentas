using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ventas;

namespace MVC
{
    public class VistaEventArgs: EventArgs
    {
        private string _idCliente;
        private Cliente _cliente;

        public VistaEventArgs(string _idCliente)
        {
            this._idCliente = _idCliente;
        }

        public VistaEventArgs(Cliente _cliente)
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
