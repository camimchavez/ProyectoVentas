using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ventas
{
    public class Stock: ISerializable
    {
        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        #endregion

        private String idStock;
        private float precio;

        public Stock(String symbol, float price)
        {
            this.idStock = symbol;
            this.precio = price;
        }

        public String IdStock
        {
            get { return idStock; }
            set { idStock = value; }
        }

        public float Precio
        {
            get { return precio; }
            set { precio = value; }
        }


        public override String ToString()
        {
            return "Stock:  " + idStock + "  " + precio;
        }

    }
}
