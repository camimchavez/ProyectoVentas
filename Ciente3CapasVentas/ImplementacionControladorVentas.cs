using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVC;
using ClienteDeRed;
using ServicioVentas;
using System.ServiceModel;
using Excepciones;

namespace Ventas
{
    class ImplementacionControladorVentas : IControladorVentas
    {
        //-------------------------------------------------------------------------
        //1 Implementar los eventos que se declararon en la interfaz IControladorVentas
        public event EventHandler<ClienteEventArgs>
                eventoDelControladorALaVistaMostrarCliente;
        public event EventHandler<TodosLosClientesEventArgs>
                eventoDelControladorALaVistaMostrarTodosLosClientes;
        public event EventHandler<ExcepcionEventArgs>
                eventoDelControladorALaVistaMostrarExcepcion;



        //private IModeloVentas modelo;
        private IContratoDelServicioVentas modelo;
        private IVistaVentas vista;

 //       public ImplementacionControladorVentas(IModeloVentas modelo, IVistaVentas vista)
        public ImplementacionControladorVentas(IContratoDelServicioVentas modelo, IVistaVentas vista)
        {
            try
            {
                //**  Asigna el parámetro modelo al atributo modelo
                this.modelo = modelo;

                //**  Asigna el parámetro vista al atributo vista
                this.vista = vista;
                //**  Registra este objeto como un listener de las acciones del usuario
                //**   en el objeto vista 
                //**   Pista - invocar a AgregarListenerAccionUsuario
                //vista.AgregarListenerAccionUsuario(this);
                vista.eventoDeLaVistaAlControladorActualizarCliente += ProcesarActualizarCliente;
                vista.eventoDeLaVistaAlControladorAgregarCliente += ProcesarAgregarClientre;
                vista.eventoDeLaVistaAlControladorBorrarCliente += ProcesarBorrarCliente;
                vista.eventoDeLaVistaAlControladorObtenerCliente += ProcesarObtenerCliente;

                vista.eventoDeLaVistaAlControladorObtenerTodoLosClientes += ProcesarObtenerTodoLosClientes;

                /*----------------------------------------------------------------------------------
                * 2 Agregar los manejadores de Eventos que manejan los cambios en la vista
                *   Los manejadores son Sub llamadas MostrarEnPantalla. Suscribir los tres
                *   métodos declarados a los eventos que van del controlador ala vista
                */
                eventoDelControladorALaVistaMostrarCliente += vista.MostrarEnPantalla;
                eventoDelControladorALaVistaMostrarTodosLosClientes += vista.MostrarEnPantalla;
                eventoDelControladorALaVistaMostrarExcepcion += vista.MostrarEnPantalla;

            }
            catch (Exception e)
            {
                ExcepcionEventArgs ex = new ExcepcionEventArgs(e);
                vista.MostrarEnPantalla(this, ex);
            }
        }

        //	métodos de call back para las acciones del usuario
        #region IControladorVentas Members

        /* ---------------------------------------------------------------
         * Obtener el Cliente por el método manejador de la acción del usuario
         * llamado por la vista de ImplementacionVistaVentas en respuesta al clic del botón 
         * para obtener el cliente que esta en la interfaz gráfica
         * o su equivalente, en la interfaz del usuario.
         * Acción - indicar el tipo de presentación en pantalla para el cliente
         * en la interfaz gráfica a través del método MostrarEnPantalla de la Vista
         */
        public void manejadorAccionGetCliente(string id)
        {
            Console.WriteLine("manejadorAccionGetCliente " + id);
            Cliente cliente = null;
            try
            {
                //**  Inicializar cliente con el objeto retornado como resultado
                //**   de invocar el método ObtenerCliente en el modelo
                cliente = modelo.ObtenerCliente(id);
                //**  Invocar el método MostrarEnPantalla de la vista  
                //**   con cliente como parámetro   
                ClienteEventArgs args = new ClienteEventArgs(cliente);
                eventoDelControladorALaVistaMostrarCliente(this, args);
            }
            catch (FaultException<ExcepcionClienteRemota> e)
            {
                throw e;
            }
            catch (Exception e)
            {
                ExcepcionEventArgs ex = new ExcepcionEventArgs(e);
                eventoDelControladorALaVistaMostrarExcepcion(this, ex);
            }
        }

        /* ---------------------------------------------------------------
         * Agregar el Cliente por el método manejador de la acción del usuario
         * llamado por ImplementacionVistaVentas en respuesta al clic sobre el botón para
         * agregar cliente en la interfaz gráfica o su equivalente, en la
         * interfaz del usuario
         * Acción - agregar el (nuevo) cliente al modelo
         */
        public void manejadorAccionAgregarCliente(Cliente c)
        {
            Console.WriteLine("manejadorAccionAgregarCliente " + c);
            try
            {
                //** Invocar al método AgregarCliente del modelo con c
                //** como parámetro
                modelo.AgregarCliente(c);
            }
            catch (FaultException<ExcepcionClienteRemota> e)
            {
                throw e;
            }
            catch (Exception e)
            {
                ExcepcionEventArgs ex = new ExcepcionEventArgs(e);
                eventoDelControladorALaVistaMostrarExcepcion(this, ex);
            }
        }

        /* ---------------------------------------------------------------
         * Borrar el Cliente con el método manejador de la acción del usuario
         * llamado por ImplementacionVistaVentas en respuesta al clic sobre el botón para
         * borrar cliente en la interfaz gráfica o su equivalente, en la
         * interfaz del usuario
         * Acción - borrar el cliente del modelo
         */
        public void manejadorAccionBorrarCliente(Cliente c)
        {
            Console.WriteLine("manejadorAccionBorrarCliente " + c);
            try
            {
                //** Invocar al método BorrarCliente del modelo con c
                //** como parámetro
                modelo.BorrarCliente(c);
            }
            catch (FaultException<ExcepcionClienteRemota> e)
            {
                throw e;
            }
            catch (Exception e)
            {
                ExcepcionEventArgs ex = new ExcepcionEventArgs(e);
                eventoDelControladorALaVistaMostrarExcepcion(this, ex);
            }
        }

        /* ---------------------------------------------------------------
         * Actualizar el Cliente por el método manejador de la acción del usuario
         * llamado por ImplementacionVistaVentas en respuesta al clic sobre el botón para
         * actualizar cliente en la interfaz gráfica o su equivalente, en la
         * interfaz del usuario
         * Acción - actualizar el cliente del modelo
         */
        public void manejadorAccionActualizarCliente(Cliente c)
        {
            Console.WriteLine("manejadorAccionActualizarCliente " + c);
            try
            {
                //**  Invocar al método ActualizarCliente del modelo con c
                //**  como parámetro
                modelo.ActualizarCliente(c);
            }
            catch (FaultException<ExcepcionClienteRemota> e)
            {
                throw e;
            }
            catch (Exception e)
            {
                ExcepcionEventArgs ex = new ExcepcionEventArgs(e);
                eventoDelControladorALaVistaMostrarExcepcion(this, ex);
            }
        }

        /* ---------------------------------------------------------------
         * Obtener todos los Clientes por el método manejador de la acción del usuario
         * llamado por la vista de ImplementacionVistaVentas en respuesta al clic del botón 
         * para obtener todos los clientes en la interfaz gráfica
         * o su equivalente, en la interfaz del usuario
         * Acción - indicar el tipo presentación en pantalla para todos los clientes
         * en la interfaz gráfica a través del método MostrarEnPantalla de la Vista
         */
        public void manejadorAccionGetTodosLosClientes()
        {
            Console.WriteLine("manejadorAccionGetTodosLosClientes ");
            Cliente[] clientes;
            try
            {
                //**  Invocar al método ObtenerTodosLosClientes del modelo
                //**  Asignar el valor retornado a clientes
                clientes = modelo.ObtenerTodosLosClientes();
                //**  Invocar el método MostrarEnPantalla de la vista  
                //**  con cliente como parámetro   
                TodosLosClientesEventArgs args = new TodosLosClientesEventArgs();
                eventoDelControladorALaVistaMostrarTodosLosClientes(this, args);
            }
            catch (FaultException<ExcepcionClienteRemota> e)
            {
                throw e;
            }
            catch (Exception e)
            {
                ExcepcionEventArgs ex = new ExcepcionEventArgs(e);
                eventoDelControladorALaVistaMostrarExcepcion(this, ex);
            }
        }

        #endregion

        private void ProcesarActualizarCliente(Object sender, VistaEventArgs args)
        {
            manejadorAccionActualizarCliente(args.Cliente);
        }
        private void ProcesarAgregarClientre(Object sender, VistaEventArgs args)
        {
            manejadorAccionAgregarCliente(args.Cliente);
        }
        private void ProcesarBorrarCliente(Object sender, VistaEventArgs args)
        {
            manejadorAccionBorrarCliente(args.Cliente);
        }
        private void ProcesarObtenerCliente(Object sender, VistaEventArgs args)
        {
            manejadorAccionGetCliente(args.IdCliente);
        }

        private void ProcesarObtenerTodoLosClientes(Object sender, VistaEventArgs args)
        {
            manejadorAccionGetTodosLosClientes();
        }

    }
}
