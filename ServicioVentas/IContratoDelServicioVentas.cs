using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ventas;
using Excepciones;

namespace ServicioVentas
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IContratoDelServicioVentas" in both code and config file together.
    [ServiceContract]
    public interface IContratoDelServicioVentas
    {
        // Métodos para registrar listeners de cambio de estado para
        // el modelo del Ventas
        /* -------------------------------------------------------------
         * Agregar un observador a la lista de objetos que serán notificados 
         * cuando un objeto(Cliente, Cartera o Stock) del modelo de Ventas 
         * altera su estado
         */
        //    event EventHandler<ModeloEventArgs> eventoDelModeloALaVista;

        // iteración 1 Métodos del segmento Cliente para el modelo de Ventas
        // Métodos del segmento Cliente de cambio de estado
        /**-------------------------------------------------------------
         * Agregar el Cliente al modelo Ventas 
         */
        [OperationContract]
        [FaultContract(typeof(ExcepcionClienteRemota))]
        void AgregarCliente(Cliente cliente);

        /**-------------------------------------------------------------
         * Borrar el cliente del modelo Ventas 
         */
        [OperationContract]
        [FaultContract(typeof(ExcepcionClienteRemota))]
        void BorrarCliente(Cliente cliente);

        /**-------------------------------------------------------------
         * Actualizar el cliente en el modelo Ventas
         */
        [OperationContract]
        [FaultContract(typeof(ExcepcionClienteRemota))]
        void ActualizarCliente(Cliente cliente);

        // Métodos del segmento Cliente para consulta del estado
        /**-------------------------------------------------------------
         * Dado un dni, retorna el cliente del modelo
         */
        [OperationContract]
        [FaultContract(typeof(ExcepcionClienteRemota))]
        Cliente ObtenerCliente(String id);


        /**-------------------------------------------------------------
         * Retorna todos los clientes en el modelo Ventas
         */
        [OperationContract]
        [FaultContract(typeof(ExcepcionClienteRemota))]
        Cliente[] ObtenerTodosLosClientes();

    }
}
