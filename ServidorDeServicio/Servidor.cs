using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServicioVentas;
using System.ServiceModel;

namespace ServidorDeServicio
{
    public class Servidor
    {
        static void Main(string[] args)
        {
            Uri direccionBase = new Uri("http://localhost:8888/");

            Type tipoDeLaInstancia =
               typeof(ImplementacionServicioVentas);

            ServiceHost servidor = new ServiceHost(tipoDeLaInstancia, direccionBase);
            using (servidor)
            {
                Type tipoDeContrato = typeof(IContratoDelServicioVentas);
                string direccionRelativa = "ServicioVentas";
                servidor.AddServiceEndpoint(tipoDeContrato,
                   new WSHttpBinding(), direccionRelativa);

                servidor.Open();
                Console.WriteLine("Servicio del Cliente de Ventas " +
                   "ejecutándose. Presione <ENTER> para salir.");
                Console.ReadLine();

                servidor.Close();
            }
        }
    }
}
