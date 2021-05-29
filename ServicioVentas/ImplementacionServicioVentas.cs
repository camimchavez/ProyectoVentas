using System;
using Modelo;
using Ventas;
using Excepciones;
using System.ServiceModel;

namespace ServicioVentas
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ImplementacionServicioVentas" in both code and config file together.
    public class ImplementacionServicioVentas : IContratoDelServicioVentas
    {
        private IDAOVentas dao = new ImplementacionDAOVentas();

        public void AgregarCliente(Cliente cliente)
        {
            string dni = cliente.Id;
            if (dni == null || dni.Length == 0)
            {
                ExcepcionClienteRemota informacion = new ExcepcionClienteRemota();
                informacion.MensajeDeError = "No se puede agregar al cliente porque el DNI tiene que tener un valor";
                informacion.Dni = "0";
                //** -----------Excepcion Remota-------------------------------
                //** 1 Lanzar una excepción remota con tipo 
                //**   Usar FaultException<ExcepcionClienteRemota> y
                //**   pasarle como parámetros informacion y "Error de operación"
                throw new FaultException<ExcepcionClienteRemota>(informacion, "Error de operación");
            }

            try
            {
                if (dao.ExisteElDni(dni))
                {
                    Console.WriteLine("Cliente Existe.");
                    ExcepcionClienteRemota informacion = new ExcepcionClienteRemota();
                    informacion.MensajeDeError="No se puede agregar al cliente porque el DNI esta registrado";
                    informacion.Dni = dni;

                    //** -----------Excepcion Remota-------------------------------
                    //** 2 Lanzar una excepción remota con tipo 
                    //**   Usar FaultException<ExcepcionClienteRemota> y
                    //**   pasarle como parámetros informacion y "Error de operación"
                    throw new FaultException<ExcepcionClienteRemota>(informacion, "Error de operación");
                }
                else
                {
                    dao.CrearCliente(cliente);
                }

                //**  Invoca el método disparaEventoCambioModelo con cliente como parámetro
                //**  Pregunta: ¿por qué es necesario este paso?
                //disparaEventoCambioModelo(cliente);
            }
            catch (FaultException<ExcepcionClienteRemota> e)
            {
                Console.WriteLine("Se produjo un error:");
                Console.WriteLine(e.Detail.MensajeDeError + ". En el cliente con DNI: "
                     + e.Detail.Dni);
                //** -----------Excepcion Remota-------------------------------
                //** 3 Volver a lanzar la excepción remota para que  
                //**   la reciba quien invoque el servicio
                throw e;
            }
            catch (ExcepcionVentas e)
            {
                Console.WriteLine("Excepción local. " + e.Message);
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepción no manejada. Error: \n" + e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void BorrarCliente(Cliente cliente)
        {
            try
            {
                dao.RemoverCliente(cliente);
            }
            catch (FaultException<ExcepcionClienteRemota> e)
            {
                Console.WriteLine("Se produjo un error:");
                Console.WriteLine(e.Detail.MensajeDeError + ". En el cliente con DNI: "
                     + e.Detail.Dni);
                //** -----------Excepcion Remota-------------------------------
                //** 4 Volver a lanzar la excepción remota para que  
                //**   la reciba quien invoque el servicio
                throw e;
            }
            catch (ExcepcionVentas e)
            {
                Console.WriteLine("Excepción local. " + e.Message);
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepción no manejada. Error: \n" + e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void ActualizarCliente(Cliente cliente)
        {
            try
            {
                dao.ModificarCliente(cliente);
            }
            catch (FaultException<ExcepcionClienteRemota> e)
            {
                Console.WriteLine("Se produjo un error:");
                Console.WriteLine(e.Detail.MensajeDeError + ". En el cliente con DNI: "
                     + e.Detail.Dni);
                //** -----------Excepcion Remota-------------------------------
                //** 5 Volver a lanzar la excepción remota para que  
                //**   la reciba quien invoque el servicio
                throw e;
            }
            catch (ExcepcionVentas e)
            {
                Console.WriteLine("Excepción local. " + e.Message);
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepción no manejada. Error: \n" + e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public Cliente ObtenerCliente(string id)
        {
            Cliente c = null;
            try
            {
                c = dao.GetCliente(id);
            }
            catch (FaultException<ExcepcionClienteRemota> e)
            {
                Console.WriteLine("Se produjo un error:");
                Console.WriteLine(e.Detail.MensajeDeError + ". En el cliente con DNI: "
                     + e.Detail.Dni);
                //** -----------Excepcion Remota-------------------------------
                //** 6 Volver a lanzar la excepción remota para que  
                //**   la reciba quien invoque el servicio
                throw e;
            }
            catch (ExcepcionVentas e)
            {
                Console.WriteLine("Excepción local. " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return c;
        }

        public Cliente[] ObtenerTodosLosClientes()
        {
            Cliente[] lista = null;
            try
            {
                lista = dao.GetTodosLosClientes();
            }
             catch (ExcepcionVentas e)
            {
                Console.WriteLine("Excepción local. " + e.Message);
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepción no manejada. Error: \n" + e.Message);
                Console.WriteLine(e.StackTrace);
            }
            return lista;
        }
    }
}
