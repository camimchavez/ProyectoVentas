using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ventas
{
    public class ExcepcionVentas : Exception
    {
        /**
 * Crea un nuevo <code>ExcepcionVentas</code> sin el mensaje del detalle.
 */
        public ExcepcionVentas():base("ExcepcionVentas")
        {
        }

        /**
         * Construye una <code>ExcepcionVentas</code> con el mensaje del detalle.
         * @param msj el mensaje del detalle.
         */
        public ExcepcionVentas(String msj) : base(msj)
        {
        }

    }
}
