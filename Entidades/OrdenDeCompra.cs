using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ventas
{
    public class OrdenDeCompra : ISerializable
    {
        private Stock stock;
        private int cantidad;

        // Constructor
        public OrdenDeCompra(Stock stock, int cantidad)
        {
            this.stock = stock;
            this.cantidad = cantidad;
        }

        public OrdenDeCompra(Stock stock)
        {
            this.stock = stock;
            this.cantidad = 0;
        }

        // métodos  
        public double calculaValor()
        {
            return cantidad * stock.Precio;
        }

        public override int GetHashCode()
        {
            return stock.IdStock.GetHashCode();
        }

        public override bool Equals(Object unObjeto)
        {
            OrdenDeCompra sr = (OrdenDeCompra)unObjeto;
            return sr.stock.IdStock.Equals(this.stock.IdStock)
              && sr.cantidad == this.cantidad;
        }

        internal Stock Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }



        public override String ToString()
        {
            return "Share:  " + stock.IdStock + "  " + cantidad;
        }


        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
