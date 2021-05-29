using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using MVC;
using ServicioVentas;

namespace Ventas
{

    class ImplementacionVistaVentas : IVistaVentas
    {

        public event EventHandler<VistaEventArgs> eventoDeLaVistaAlControladorAgregarCliente;
        public event EventHandler<VistaEventArgs> eventoDeLaVistaAlControladorActualizarCliente;
        public event EventHandler<VistaEventArgs> eventoDeLaVistaAlControladorObtenerCliente;
        public event EventHandler<VistaEventArgs> eventoDeLaVistaAlControladorBorrarCliente;
        public event EventHandler<VistaEventArgs> eventoDeLaVistaAlControladorObtenerTodoLosClientes;

        //private IModeloVentas modeloVentas;
        private IContratoDelServicioVentas modeloVentas;
        private ArrayList controladoresDeVentas = new ArrayList(10);

        private Form1 gui;

 //       public ImplementacionVistaVentas(IModeloVentas modelo, Form1 gui)
        public ImplementacionVistaVentas(IContratoDelServicioVentas modelo, Form1 gui)
        {
            Console.WriteLine("Creando ImplementacionVistaVentas");
            try
            {
                //** 1 Asignar modelo a el atributo modeloVentas
                modeloVentas = modelo;

                //** 2 Invocar el método agregarListenerDeCambio con this
                //     para pasar esta instancia de ImplVistaVentas
                //     como parámetro
                //modelo.AgregarListenerDeCambio(this);

                // modelo.eventoDelModeloALaVista += ProcesarCambioEnElModelo;

                Ventas.Form1.DisparaEventoAgregarCliente opAgregar = AgregarCliente;
                Ventas.Form1.DisparaEventoActualizarCliente opActualizar = ActualizarCliente;
                Ventas.Form1.DisparaEventoBorrarCliente opBorrar = BorrarCliente;
                Ventas.Form1.DisparaEventoObtenerCliente opObtener = ObtenerCliente;


                // invocación de los métodos que reciben las referencias en la interfaz
                // gráfica a los métodos de esta clase que disparan los eventos asociados a la vista
                // que se declararon en la interfaz IVistaVentas
                gui.RegistraEventosVista(opAgregar);
                gui.RegistraEventosVista(opActualizar);
                gui.RegistraEventosVista(opBorrar);
                gui.RegistraEventosVista(opObtener);


            }
            catch (Exception e)
            {
                Console.WriteLine("ImplementacionVistaVentas constructor " + e.Message);
            }
            this.gui = gui;
        }

        public void ProcesarCambioEnElModelo(Object sender, ModeloEventArgs args)
        {
            this.ManejadorCambioCliente(args.Cliente);
            eventoDeLaVistaAlControladorObtenerTodoLosClientes(this, null);
        }


        // Métodos para registrar los listeners de acciones del usuario
        /* agregar requerintes a la lista de objetos a ser notificados de
         * las acciones del usuario ingresadas a través de una interfaz como la GUI
         * Las acciones del usuario para el segmento del cliente son agregar,
         * borrar, actualizar, obtener y obtener todos los clientes. Hay acciones
         * del usuario similares para los segmentos de cartera y stock
         */

        #region IVistaVentas Members

        public void MostrarEnPantalla(Object sender, MVC.ClienteEventArgs args)
        {
            gui.MostrarObjetoPorPantalla(args);
        }

        public void MostrarEnPantalla(Object sender, MVC.TodosLosClientesEventArgs args)
        {
            gui.MostrarObjetoPorPantalla(args);
        }

        public void MostrarEnPantalla(Object sender, MVC.ExcepcionEventArgs args)
        {
            gui.MostrarObjetoPorPantalla(args);
        }

        public void ManejadorCambioCliente(Cliente cli)
        {
            Console.WriteLine("ImplementacionVistaVentas.manejadorCambioCliente" + cli);
            try
            {
                ClienteEventArgs args = new ClienteEventArgs(cli);
                gui.MostrarObjetoPorPantalla(args);
            }
            catch (Exception e)
            {
                Console.WriteLine("ImplementacionVistaVentas: error en manejadorCambioCliente " + e);
            }

        }

        #endregion

        // Métodos para disparar los eventos de acciones del usuario
        // ingresadas a través de una interfaz como la GUI
        // Las acciones del usuario para el segmento del cliente son agregar,
        // borrar, actualizar, obtener y obtener todos los clientes. Hay acciones
        // del usuario similares para los segmentos de cartera y stock
        //
        private void ActualizarCliente(MVC.VistaEventArgs args)
        {
            //2 Disparar el evento que maneja el controlador para actualizar al cliente
            //  Ejemplo: RaiseEvent eventoDeLaVistaAlControladorActualizarCliente(this, args)
            eventoDeLaVistaAlControladorActualizarCliente(this, args);
        }

        private void BorrarCliente(MVC.VistaEventArgs args)
        {
            //3 Disparar el evento que maneja el controlador para borrar al cliente
            eventoDeLaVistaAlControladorBorrarCliente(this, args);
        }

        private void AgregarCliente(MVC.VistaEventArgs args)
        {
            //4 Disparar el evento que maneja el controlador para agregar un cliente
            eventoDeLaVistaAlControladorAgregarCliente(this, args);
        }

        private void ObtenerCliente(MVC.VistaEventArgs args)
        {
            //5 Disparar el evento que maneja el controlador para obtener un cliente
            eventoDeLaVistaAlControladorObtenerCliente(this, args);
        }

        private void ObtenerTodosLosCliente(MVC.VistaEventArgs args)
        {
            //6 Disparar el evento que maneja el controlador para obtener todos los clientes
            eventoDeLaVistaAlControladorObtenerTodoLosClientes(this, args);
        }

    }
}
