using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVC;

namespace Ventas
{
    public interface IModeloVentas
    {
        // Métodos para registrar listeners de cambio de estado para
        // el modelo del Ventas
        /* -------------------------------------------------------------
         * Agregar un observador a la lista de objetos que serán notificados 
         * cuando un objeto(Cliente, Cartera o Stock) del modelo de Ventas 
         * altera su estado
         */
        event EventHandler<ModeloEventArgs> eventoDelModeloALaVista;

        // iteración 1 Métodos del segmento Cliente para el modelo de Ventas
        // Métodos del segmento Cliente de cambio de estado
        /**-------------------------------------------------------------
         * Agregar el Cliente al modelo Ventas 
         */
        void AgregarCliente(Cliente cliente);

        /**-------------------------------------------------------------
         * Borrar el cliente del modelo Ventas 
         */
        void BorrarCliente(Cliente cliente);

        /**-------------------------------------------------------------
         * Actualizar el cliente en el modelo Ventas
         */
        void ActualizarCliente(Cliente cliente);

        // Métodos del segmento Cliente para consulta del estado
        /**-------------------------------------------------------------
         * Dado un dni, retorna el cliente del modelo
         */
        Cliente ObtenerCliente(String id);


        /**-------------------------------------------------------------
         * Retorna todos los clientes en el modelo Ventas
         */
        Cliente[] ObtenerTodosLosClientes();

        // Segmento Cartera - Se completará en una iteración futura
        // Agregar métodos de cambio de estado al segmento Cartera
        // Agregar métodos de consulta de estado al segmento Cartera

        // Segmento Stock - Se completará en una iteración futura
        // Agregar métodos de cambio de estado al segmento Stock
        // Agregar métodos de consulta de estado al segmento Stock
    }
}
