using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using Modelo;
using MVC;

namespace Ventas
{
    public class ImplementacionModeloVentas : IModeloVentas
    {
        public event EventHandler<ModeloEventArgs> eventoDelModeloALaVista;

        private List<IVistaVentas> suscriptoresDeCambio = new List<IVistaVentas>(10);
        private IDAOVentas dao;


        public ImplementacionModeloVentas(IDAOVentas dao)
        {
            this.dao = dao;
        }


        /**-----------------------------------------------------------
        * Este método notifica a todos los sucriptores de VistaVentas registrados
        * que un objeto cliente ha cambiado.
        */
        private void disparaEventoCambioModelo(Cliente cli)
        {
            foreach (IVistaVentas v in suscriptoresDeCambio)
            {
                try
                {
                    Console.WriteLine("Ciclo en el Modelo:disparaEventoCambioModelo");
                    v.ManejadorCambioCliente(cli);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            // Crear una copia del evento para evitar un race condition
            // (más de un proceso tratando de usar el mismo evento)
            EventHandler<ModeloEventArgs> manejador = eventoDelModeloALaVista;
            if (manejador != null)
            {
                ModeloEventArgs args = new ModeloEventArgs(cli);
                manejador(this, args);
            }

        }



        #region IModeloVentas Members

        // Métodos de registración de suscriptores de cambio de estado del modelo
        /**-----------------------------------------------------------
         * Agregar un suscriptor a la lista de objetos que serán
         * notificados cuando un objeto(Cliente, Cartera or Stock) 
         * en el modelo alteren su estado   
         */
        public void AgregarListenerDeCambio(IVistaVentas vv)
        {
            //** 1 agregar vv a suscriptoresDeCambio usando el método Add
            suscriptoresDeCambio.Add(vv);
        }

        // Iteración 1 Métodos del segmento de Cliente en el modelo
        // Métodos de cambio de estado del cliente
        /**----------------------------------------------------------
         * Agrega el Cliente al modelo 
         */
        public void AgregarCliente(Cliente cliente)
        {
            try
            {
                if (dao.ExisteElDni(cliente.Id))
                {
                    throw new ExcepcionVentas("DNI Duplicado");
                }
                else
                {
                    dao.CrearCliente(cliente);
                }

                //**  Invoca el método disparaEventoCambioModelo con cliente como parámetro
                //**  Pregunta: ¿por qué es necesario este paso?
                disparaEventoCambioModelo(cliente);
            }
            catch (Exception e)
            {
                Console.WriteLine("ImplementacionModeloVentas.agregaCliente\n" + e.Message);
                Console.WriteLine(e.StackTrace);
                throw new ExcepcionVentas("ImplementacionModeloVentas.agregaCliente\n" + e.Message);
            }
        }

        /**-------------------------------------------------------------
         * Borra al cliente del modelo
         */
        public void BorrarCliente(Cliente cliente)
        {
            try
            {
                if (!dao.ExisteElDni(cliente.Id))
                {
                    throw new ExcepcionVentas("El DNI no existe");
                }
                else
                {
                    dao.RemoverCliente(cliente);
                }

                //**  Invoca el método disparaEventoCambioModelo con cliente como parámetro
                //**  Pregunta: ¿por qué es necesario este paso?
                disparaEventoCambioModelo(cliente);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new ExcepcionVentas
                  ("ImplementacionModeloVentas.borrarCliente\n" + e);
            }
        }

        /**-------------------------------------------------------------
         * Actualizar el cliente en el modelo
         */
        public void ActualizarCliente(Cliente cliente)
        {
            try
            {
                if (!dao.ExisteElDni(cliente.Id))
                {
                    throw new ExcepcionVentas("El DNI no existe");
                }
                else
                {
                    dao.ModificarCliente(cliente);
                }

                //**  Invoca el método disparaEventoCambioModelo con cliente como parámetro
                //**  Pregunta: ¿por qué es necesario este paso?
                disparaEventoCambioModelo(cliente);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new ExcepcionVentas
                  ("ImplementacionModeloVentas.actualizarCliente\n" + e);
            }
        }

        // Segmento de los métodos de consulta del estado del Cliente
        /**-------------------------------------------------------------
         * Dado un dni, retorna el Cliente del modelo
         */
        public Cliente ObtenerCliente(string dni)
        {
            try
            {
                Cliente cli = null;

                cli = dao.GetCliente(dni);

                if (cli == null)
                {
                    // si la consulta falla 
                    throw new ExcepcionVentas("Registro para " + dni +
                      " no se encontró");
                }

                // returna cli
                return cli;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new ExcepcionVentas("ImplementacionModeloVentas.ObtenerCliente\n" + e);
            }

        }

        public Cliente[] ObtenerTodosLosClientes()
        {
            Cliente[] todosCli = null;

            try
            {
                    todosCli = dao.GetTodosLosClientes();
            }
            catch (SqlException e)
            {
                todosCli = null;
                Console.WriteLine(e.StackTrace);
                throw new ExcepcionVentas("ImplementacionModeloVentas.getTodosLosClientes\n" + e);
            }
            return todosCli;

        }

        #endregion


    }
}
