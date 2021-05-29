using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;

namespace Ventas
{
    public class Cartera : ISerializable
    {

        private Cliente cli;
        private List<OrdenDeCompra> ordenesDeCompra;

        public Cartera(Cliente cli, List<OrdenDeCompra> ordenesDeCompra)
        {
            this.cli = cli;
            this.ordenesDeCompra = ordenesDeCompra;
        }

        public Cartera(Cliente cli)
        {
            this.cli = cli;
            this.ordenesDeCompra = new List<OrdenDeCompra>(10);
        }

        public Cliente Cliente
        {
            get { return cli; }
        }

        public List<OrdenDeCompra> ListaOrdenesDeCompra
        {
            get { return ordenesDeCompra; }
            set { ordenesDeCompra = value; }
        }



        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
