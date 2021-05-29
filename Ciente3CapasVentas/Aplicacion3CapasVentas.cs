using System;
using System.Windows.Forms;
using Ventas;
using ClienteDeRed;
using ServicioVentas;


namespace Ventas
{
    static class Aplicacion3CapasVentas
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
   //         String hostBDD = "localhost";

            try
            {
 //               IDAOVentas dao = new ImplementacionDAOVentas(hostBDD);
 //               IModeloVentas modelo = new ImplementacionModeloVentas(dao);
                ClienteDelServicioVentas cliServicioRed = new ClienteDelServicioVentas();
                IContratoDelServicioVentas modelo = cliServicioRed.Proxy;

                Form1 gui1 = new Form1(modelo);

                IVistaVentas vista1 = new ImplementacionVistaVentas(modelo, gui1);
                IControladorVentas con1 = new ImplementacionControladorVentas(modelo, vista1);

                Form1 gui2 = new Form1(modelo);

                IVistaVentas vista2 = new ImplementacionVistaVentas(modelo, gui2);
                IControladorVentas con2 = new ImplementacionControladorVentas(modelo, vista2);

                gui1.Show();
                gui2.Show();
                Application.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}