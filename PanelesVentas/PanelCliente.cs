using System;
using System.Windows.Forms;
using Ventas;
using MVC;
using ServicioVentas;

namespace Paneles
{
    public partial class PanelCliente : UserControl, IPanelVentas
    {
        public event EventHandler<VistaEventArgs> EventoActualizarCliente;
        public event EventHandler<VistaEventArgs> EventoBorrarCliente;
        public event EventHandler<VistaEventArgs> EventoAgregarCliente;
        public event EventHandler<VistaEventArgs> EventoObtenerCliente;

//        private IModeloVentas modelo;
        private IContratoDelServicioVentas modelo;
        private IControladorVentas controlador;
        private String dniCliente;

        public PanelCliente()
        {
            InitializeComponent();
        }


        #region IPanelVentas Members

        public void RegistrarControlador(Ventas.IControladorVentas controlador)
        {
            this.controlador = controlador;
        }

//        public void RegistrarModelo(Ventas.IModeloVentas modelo)
        public void RegistrarModelo(IContratoDelServicioVentas modelo)
        {
            this.modelo = modelo;
        }

        public void MostrarPorPantalla(object obj)
        {
            Cliente cliente;
            if (obj is Cliente)
            {
                cliente = (Cliente)obj;
                dniCliente = textDni.Text.Trim();
                // if cust.getId equals custId on the CustomerPanel
                // then display the customer object on the CustomerPanel
                if (dniCliente.Equals(cliente.Id))
                {
                    textNombre.Text = cliente.Nombre;
                    textDireccion.Text = cliente.Direccion;
                }
            }
        }

        public void Refrescar()
        {
            // Recuperar el id correspondiente al cliente de su respectivo TextBox
            try
            {
                // (Si no está vacío) Obetener el corrspondiente objeto Cliente 
                // desde el modelo para mostrar sus detalles en el panel
                if (textDni.Text.Trim() != "")
                {
                    //**1 Obtener el Cliente según el correspondiente ID
                    //    y asignárselo a cliente.                
                    Cliente cliente = modelo.ObtenerCliente(textDni.Text.Trim());
                    //**2 Actualizar el TextBox de Dirección con el valor en cliente
                    textDireccion.Text = cliente.Direccion;
                    //**3 Actualizar el TextBox de Nombre con el valor en cliente
                    textNombre.Text = cliente.Nombre;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        //Métodos manejadores de eventos--------------------------------------
        // Suscriptor de eventos con el botón "botonObtener" de la gui.
        // Informar al controlador la acción

        private void botonObtener_Click(object sender, EventArgs e)
        {
            // Este método es llamado cuando el botón "Obtener Cliente"
            // es presionado por el usuario
            String idCliente;
            Console.WriteLine("PanelCliente: botonObtener_Click");

            //  Asignar idCliente con el valor del dni del cliente (DNI) en
            //  la gui. Usar el método GetDniCliente()
            idCliente = GetDniCliente();

            // Invocar al método manejadorAccionGetCliente en el controlador
            //controlador.manejadorAccionGetCliente(idCliente);

            // Crear una copia del evento para evitar un race condition
            // (más de un proceso tratando de usar el mismo evento)
            EventHandler<VistaEventArgs> manejador = EventoObtenerCliente;
            if (manejador != null)
            {
                VistaEventArgs args = new VistaEventArgs(idCliente);
                manejador(this, args);
            }
        }

        private void botonModificar_Click(object sender, EventArgs e)
        {
            String idCliente;

            Console.WriteLine("PanelCliente: botonModificar_Click");
            idCliente = GetDniCliente();

            Cliente cliente = new Cliente(idCliente, GetNombreCliente(), GetDireccionCliente());
            //controlador.manejadorAccionActualizarCliente(cliente);

            // Crear una copia del evento para evitar un race condition
            // (más de un proceso tratando de usar el mismo evento)
            EventHandler<VistaEventArgs> manejador = EventoActualizarCliente;
            if (manejador != null)
            {
                VistaEventArgs args = new VistaEventArgs(cliente);
                manejador(this, args);
            }
        }

        private void botonAgregar_Click(object sender, EventArgs e)
        {
            // Este método es llamado cuando el botón "Agregar Cliente"
            // es presionado por el usuario     	
            String idCliente;

            Console.WriteLine("PanelCliente: botonAgregar_Click");
            idCliente = GetDniCliente();

            Cliente cliente = new Cliente(idCliente, GetNombreCliente(), GetDireccionCliente());
            //controlador.manejadorAccionAgregarCliente(cliente);

            // Crear una copia del evento para evitar un race condition
            // (más de un proceso tratando de usar el mismo evento)
            EventHandler<VistaEventArgs> manejador = EventoAgregarCliente;
            if (manejador != null)
            {
                VistaEventArgs args = new VistaEventArgs(cliente);
                manejador(this, args);
            }
        }

        private void botonBorrar_Click(object sender, EventArgs e)
        {
            // Este método es llamado cuando el botón "Borrar Cliente"
            // es presionado por el usuario

            String idCliente;

            Console.WriteLine("PanelCliente: botonBorrar_Click");
            idCliente = GetDniCliente();
            Cliente cliente = new Cliente(GetDniCliente(), GetNombreCliente(), GetDireccionCliente());
            //controlador.manejadorAccionBorrarCliente(cliente);

            // Crear una copia del evento para evitar un race condition
            // (más de un proceso tratando de usar el mismo evento)
            EventHandler<VistaEventArgs> manejador = EventoBorrarCliente;
            if (manejador != null)
            {
                VistaEventArgs args = new VistaEventArgs(cliente);
                manejador(this, args);
            }

            Console.WriteLine("ImplementacionVistaVentas: manejadorBorrarCliente");
        }


        // Métodos de ayuda
        protected String GetDniCliente()
        {
            return textDni.Text.Trim();
        }

        protected String GetNombreCliente()
        {
            return textNombre.Text.Trim();
        }

        protected String GetDireccionCliente()
        {
            return textDireccion.Text.Trim();
        }

    }
}
