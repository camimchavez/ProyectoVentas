using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVC;

namespace Ventas
{
    public interface IVistaVentas
    {
        // Métodos para registrar los suscriptores de acciones del usuario
        /* agregar requirentes a la lista de objetos a ser notificados de
        * las acciones del usuario ingresadas a través de una interfaz como la GUI
        * Las acciones del usuario para el segmento del cliente son agregar,
        * borrar, actualizar, obtener y obtener todos los clientes. Hay acciones
        * del usuario similares para los segmentos de cartera y stock
        */
        event EventHandler<VistaEventArgs> eventoDeLaVistaAlControladorAgregarCliente;
        event EventHandler<VistaEventArgs> eventoDeLaVistaAlControladorActualizarCliente;
        event EventHandler<VistaEventArgs> eventoDeLaVistaAlControladorObtenerCliente;
        event EventHandler<VistaEventArgs> eventoDeLaVistaAlControladorBorrarCliente;

        event EventHandler<VistaEventArgs> eventoDeLaVistaAlControladorObtenerTodoLosClientes;

        //Métodos de utilidad para mostrar en pantalla un requerimiento de 
        //selección del vendedor
        /* ---------------------------------------------------------------
         * muestra por pantalla la página especificada por el controlador
         */
        void MostrarEnPantalla(Object sender, MVC.ClienteEventArgs args);
        void MostrarEnPantalla(Object sender, MVC.TodosLosClientesEventArgs args);
        void MostrarEnPantalla(Object sender, MVC.ExcepcionEventArgs args);

        // iteracion 1 -- Métodos del segmento del Cliente para la vista de los vendedores
        /* ---------------------------------------------------------------
         * Métodos de callback para manejar la notificación de los cambios 
         * de estado del cliente para el modelo Ventas
         */
        void ManejadorCambioCliente(Cliente cliente);

        // Segmento Cartera - Se completará en una iteración futura
        // Agregar un método para manejar las notificaciones de cambio de cartera 
        // desde el modelo Ventas

        // Segmento Stock - Se completará en una iteración futura
        // Agregar un método para manejar las notificaciones de cambio de stock 
        // desde el modelo Ventas

    }
}
