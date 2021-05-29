using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVC;

namespace Ventas
{
    public interface IControladorVentas
    {
        // ---------------------------------------------------------------
        /** 1 Declarar los eventos que van del controlador a la vista para que
        *   manejen cada uno respectivamente los argumentos ClienteEventArgs,
        *   TodosLosClientesEventArgs y ExcepcionEventArgs
        */
        event EventHandler<ClienteEventArgs> eventoDelControladorALaVistaMostrarCliente;
        event EventHandler<TodosLosClientesEventArgs> eventoDelControladorALaVistaMostrarTodosLosClientes;
        event EventHandler<ExcepcionEventArgs> eventoDelControladorALaVistaMostrarExcepcion;


        // ---------------------------------------------------------------
        /** Agregar el Cliente por el método manejador de la acción del usuario
        * llamado por ImplVistaVentas en respuesta al clic sobre el botón para
        * agregar cliente en la interfaz gráfica o su equivalente en la
        * interfaz del usuario
        * acción - agregar el (nuevo) cliente del modelo
        */
        void manejadorAccionAgregarCliente(Cliente c);

        // ---------------------------------------------------------------
        /** Borrar el Cliente por el método manejador de la acción del usuario
        * llamado por ImplVistaVentas en respuesta al clic sobre el botón para
        * borrar cliente en la interfaz gráfica o su equivalente en la
        * interfaz del usuario
        * acción - borrar el cliente del modelo
        */
        void manejadorAccionBorrarCliente(Cliente c);

        // ---------------------------------------------------------------
        /** Actualizar el Cliente por el método manejador de la acción del usuario
        * llamado por ImplVistaVentas en respuesta al clic sobre el botón para
        * actualizar cliente en la interfaz gráfica o su equivalente en la
        * interfaz del usuario
        * acción - actualizar el cliente del modelo
        */
        void manejadorAccionActualizarCliente(Cliente c);


        // ---------------------------------------------------------------
        /** Obtener todos los Clientes por el método manejador de la acción del usuario
        * llamado por la vista de ImplVistaVentas en respuesta al clic del botón 
        * para obtener todos los clientes en la interfaz gráfica
        * o su equivalente en la interfaz del usuario
        * acción - indicar el tipo presentación en pantalla para todos los clientes
        * en la interfaz gráfica a través del método mostrarEnPantalla de VistaVentas
        */
        void manejadorAccionGetTodosLosClientes();

        // Segmento de Cartera - se realizará en una iteración futura
        // Agregar un método para manejar las notificaciones de las acciones del usuario
        // relacionadas con la cartera desde VistaVentas

        //Segmento de Stock - se realizará en una iteración futura
        //Agregar un método para manejar las notificaciones de las acciones del usuario
        //relacionadas con el stock desde VistaVentas

    }
}
