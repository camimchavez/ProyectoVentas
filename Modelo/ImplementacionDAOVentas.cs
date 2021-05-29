using System;
using System.Data.SqlClient;
using Ventas;
using System.Collections;
using Excepciones;
using System.ServiceModel;

namespace Modelo
{
    public class ImplementacionDAOVentas : IDAOVentas
    {
        SqlConnection laConexion;

        private String nomServer = @"localhost\SQLEXPRESS";
        private String nombreBaseDeDatos = "Stock";
        private const String seguridad = "True";

        public ImplementacionDAOVentas(string instancia)
        {
            nomServer = instancia;
            try
            {
                //** 1 El nombre de la instancia de MS SQL Server es
                //**   el servidor donde se encuentra la BDD a Conectar       	
                laConexion = retornaConexion();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Construcción de ImplementacionDAOVentasCS: " + e.Message);
                Console.WriteLine(e.StackTrace);

                throw new ExcepcionVentas("Error no manejado");
            }
        }

        public ImplementacionDAOVentas()
        {
            try
            {
                //** 1 El nombre de la instancia de MS SQL Server es
                //**   el servidor donde se encuentra la BDD a Conectar       	
                laConexion = retornaConexion();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Construcción de ImplementacionDAOVentasCS: " + e.Message);
                Console.WriteLine(e.StackTrace);

                throw new ExcepcionVentas("Error no manejado");
            }
        }

        private SqlConnection retornaConexion()
        {
            try
            {
                //** 3 Asignar al atributo laConexion un objeto SqlConnection
                //**   invocando al constructor
                laConexion = new SqlConnection(getUrlConexion());
                Console.WriteLine("Conexión Exitosa!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Seguimiento en getConexion() : " + e.Message);
                Console.WriteLine(e.StackTrace);

                throw new ExcepcionVentas("Error no manejado");
            }
            return laConexion;
        }


        private String getUrlConexion()
        {
            //** 1 Construir el string para la urlBase de acceso.
            //**   Para que sea más flexible lo devuelve un método
            return "Server = " + nomServer + ";DataBase=" + nombreBaseDeDatos + ";Integrated Security=" + seguridad + ";";
        }


        #region IDAOVentas Members

        public void CrearCliente(Ventas.Cliente cliente)
        {
            SqlConnection con = null;
            String dni = null;
            String nombre = null;
            String direccion = null;
            String requerimiento;
            try
            {
                //** 1 Asigna el DNI con el del objeto cliente
                dni = cliente.Id;
                //** 2 Asigna nombre con el del objeto cliente
                nombre = cliente.Nombre;
                //** 3 Asigna direccion con el del objeto cliente
                direccion = cliente.Direccion;
                //Pregunta: ¿cuál es el propósito de la siguiente línea de código
                if (ExisteElDni(dni))
                {
                    // throw new ExcepcionVentas("DNI Duplicado");
                    ExcepcionClienteRemota informacion = GeneraInformacionExcepcionClienteRemota(
                       "DNI Duplicado", dni, nombre, direccion);
                    //** -----------Excepción Remota-------------------------------
                    //** 1 Lanzar una excepción remota con tipo 
                    //**   Usar FaultException<ExcepcionClienteRemota> y
                    //**   pasarle como parámetros informacion y "Error de operación"
                    throw new FaultException<ExcepcionClienteRemota>(informacion, "Error de operación");
                }

                //** 4 Asigna con invocando al método retornaConexion()
                //**   por ejemplo, ver un paso similar en el método existeDNI
                con = retornaConexion();

                //** 5 Abre la conexión a la BDD
                con.Open();

                // La siguiente sentencia crea el string de SQL requerido para el INSERT
                requerimiento =
                  "INSERT INTO Cliente (DNI, NomCliente, Direccion) VALUES ("
                  + "'" + dni + "'" + ","
                  + "'" + nombre + "'" + ","
                  + "'" + direccion + "'" + ")";

                //** 6 Crear una nueva instancia del comando con una consulta y la conexión
                //**   Llamar al método ExecuteNonQuery para enviar el comando
                SqlCommand cmd = new SqlCommand(requerimiento, con);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine("ImplementacionDAOVentasCS.crearCliente\n" + e.Message);
                Console.WriteLine(e.StackTrace);
                //throw new ExcepcionVentas("ImplementacionDAOVentasCS.crearCliente\n" + e.Message);
                ExcepcionClienteRemota informacion = GeneraInformacionExcepcionClienteRemota(
                    "Error al insertar en la BDD", dni, nombre, direccion);
                //** -----------Excepción Remota-------------------------------
                //** 2 Lanzar una excepción remota con tipo 
                //**   Usar FaultException<ExcepcionClienteRemota> y
                //**   pasarle como parámetros informacion y "Error de operación"

                throw new FaultException<ExcepcionClienteRemota>(informacion, "Error de operación");
            }
            finally
            {
                // Cerrar la conexión
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public void RemoverCliente(Ventas.Cliente cliente)
        {
            SqlConnection con = null;
            try
            {
                String dni = null;
                String requerimiento;

                //** 1 Asigna el dni con el del objeto cliente
                dni = cliente.Id;

                //Pregunta: ¿cuál es el propósito de las siguientes 4 líneas de código
                if (!ExisteElDni(dni))
                {
                    //throw new ExcepcionVentas("El registro para" + dni +
                    //  " no se encontró");
                    ExcepcionClienteRemota informacion = GeneraInformacionExcepcionClienteRemota(
                       "El registro no se encontró", dni, null, null);

                    //** -----------Excepción Remota-------------------------------
                    //** 3 Lanzar una excepción remota con tipo 
                    //**   Usar FaultException<ExcepcionClienteRemota> y
                    //**   pasarle como parámetros informacion y "Error de operación"
                    throw new FaultException<ExcepcionClienteRemota>(informacion, "Error de operación");
                }


                //** 2 Asigna con invocando al método retornaConexion()
                //**   por ejemplo, ver un paso similar en el método existeDNI
                con = retornaConexion();

                //** 3 Abre la conexión
                con.Open();

                // La siguiente línea crea el string SQL DELETE requerido
                // para borrar filas de la tabla Distribucion
                requerimiento =
                  "DELETE FROM Distribucion WHERE DNI=" + "'" + dni + "'";

                // La siguiente línea crea el string SQL DELETE requerido
                // para borrar filas de la tabla Cliente
                requerimiento =
                  "DELETE FROM Cliente WHERE DNI=" + "'" + dni + "'";

                //** 4 Ejecuta el método ExecuteNonQuery(requerimiento, com) 
                //**   para realizar la operación
                SqlCommand cmd = new SqlCommand(requerimiento, con);
                cmd.ExecuteNonQuery();

                //** 5 Usando la propiedad Nombre ingresa el string 
                //**   "- cliente borrado -" en el campo nombre 
                //**   del objeto cli. 
                cliente.Nombre = "- cliente borrado -";

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                throw new ExcepcionVentas
                  ("ImplementacionDAOVentasCS.removerCliente\n" + e);
            }
            finally
            {
                // Cerrar la conexión
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public void ModificarCliente(Ventas.Cliente cliente)
        {
            SqlConnection con = null;
            try
            {
                String dni = null;
                String nombre = null;
                String direccion = null;
                String requerimiento;

                //** 1 Asigna el dni con el del objeto cli
                dni = cliente.Id;
                //** 2 Asigna nombre con el del objeto cli
                nombre = cliente.Nombre;
                //** 3 Asigna direccion con el del objeto cli
                direccion = cliente.Direccion;
                //Pregunta: ¿cuál es el propósito de las siguientes líneas de código?
                if (!ExisteElDni(dni))
                {
                    //throw new ExcepcionVentas("Registro para " + dni +
                    //  " no se encontró");
                    ExcepcionClienteRemota informacion = GeneraInformacionExcepcionClienteRemota(
                       "El registro no se encontró", dni, nombre, direccion);
                    //** -----------Excepción Remota-------------------------------
                    //** 4 Lanzar una excepción remota con tipo 
                    //**   Usar FaultException<ExcepcionClienteRemota> y
                    //**   pasarle como parámetros informacion y "Error de operación"

                    throw new FaultException<ExcepcionClienteRemota>(informacion, "Error de operación");
                }

                //** 4 Asigna con invocando al método retornaConexion()
                //**   por ejemplo, ver un paso similar en el método existeDNI
                con = retornaConexion();
                con.Open();

                // La siguiente línea crea el string SQL UPDATE requerido
                requerimiento =
                  "UPDATE Cliente SET "
                  + " NomCliente=" + "'" + nombre + "'" + ","
                  + " Direccion=" + "'" + direccion + "'"
                  + " WHERE DNI=" + "'" + dni + "'";

                //** 5 Ejecuta el método ExecuteNonQuery(requerimiento, com) 
                //**   para realizar la operación
                SqlCommand cmd = new SqlCommand(requerimiento, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new ExcepcionVentas
                  ("ImplementacionDAOVentasCS.ModificarCliente\n" + e);
            }
            finally
            {
                // Cerrar la conexión
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public Cliente GetCliente(string dni)
        {
            SqlConnection con = null;
            try
            {
                String nombre = null;
                String direccion = null;
                SqlDataReader rdr = null;
                String requerimiento;
                Cliente cli = null;

                //** 1 Asigna con invocando al método retornaConexion()
                //**   por ejemplo, ver un paso similar en el método existeDNI
                con = retornaConexion();
                con.Open();

                // La siguiente línea crea el string SQL SELECT requerido
                requerimiento =
                  "SELECT NomCliente, Direccion FROM Cliente "
                   + " WHERE DNI=" + "'" + dni + "'";

                //** 2 Ejecuta el método ExecuteNonQuery(requerimiento, com) 
                //**   para realizar la operación
                SqlCommand cmd = new SqlCommand(requerimiento, con);
                cmd.ExecuteNonQuery();

                //** 3  Llamar al método que ejecuta el lector para obtener los resultados
                rdr = cmd.ExecuteReader();

                // La siguiente sentencia revisa si la consulta fue exitosa
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        //** 4 Asigna nombre con el valor retornado por el 
                        //**   método ToString() en el objeto resultado rdr
                        nombre = rdr[0].ToString();

                        //** 5 Asigna direccion con el valor retornado por el 
                        //**   método ToString() en el objeto resultado rdr
                        direccion = rdr[1].ToString();

                        //** 6 Crea un objeto Cliente usando ((dni, nombre, direccion) 
                        //**   y asigna este objeto a cli
                        cli = new Cliente(dni, nombre, direccion);
                    }
                }
                else
                {
                    // si la consulta falla 
                    //throw new ExcepcionVentas("Registro para " + dni +
                    //  " no se encontró");
                    Console.WriteLine("ImplementacionDAOVentasCS.ObtenerCliente\n");
                    ExcepcionClienteRemota informacion = GeneraInformacionExcepcionClienteRemota(
                        "Error al consultar la BDD", dni, nombre, direccion);
                    //** -----------Excepción Remota-------------------------------
                    //** 5 Lanzar una excepción remota con tipo 
                    //**   Usar FaultException<ExcepcionClienteRemota> y
                    //**   pasarle como parámetros informacion y "Error de operación"
                    throw new FaultException<ExcepcionClienteRemota>(informacion, "Error de operación");
                }
                // returna cli
                return cli;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                throw new ExcepcionVentas("ImplementacionDAOVentasCS.GetCliente\n" + e.Message);
            }
            finally
            {
                // Cerrar la conexión
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public Cliente[] GetTodosLosClientes()
        {
            String dni;
            String nombre;
            String direccion;
            SqlConnection con = null;
            SqlDataReader rdr = null;
            String requerimiento;
            Cliente cli = null;
            Cliente[] todosCli;
            Cliente[] temp = new Cliente[1];
            ArrayList laLista = new ArrayList(1);
            try
            {
                //** 1 Asigna con invocando al método retornaConexion()
                //**   por ejemplo, ver un paso similar en el método existeDNI
                con = retornaConexion();
                con.Open();

                // La siguiente línea crea el string SQL SELECT requerido
                requerimiento = "SELECT * FROM Cliente";

                //** 2 Ejecuta el método ExecuteNonQuery(requerimiento, com) 
                //**   para realizar la operación
                SqlCommand cmd = new SqlCommand(requerimiento, con);
                cmd.ExecuteNonQuery();

                //** 3  Llamar al método que ejecuta el lector para obtener los resultados
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //** 4 Asigna dni con el valor retornado por el 
                    //**   método ToString() en el objeto resultado rdr
                    dni = rdr[0].ToString();

                    //** 5 Asigna nombre con el valor retornado por el 
                    //**   método ToString() en el objeto resultado rdr
                    nombre = rdr[1].ToString();

                    //** 6 Asigna direccion con el valor retornado por el 
                    //**   método ToString() en el objeto resultado rdr
                    direccion = rdr[2].ToString();

                    //** 7 Crea un objeto Cliente usando ((dni, nombre, direccion) 
                    //**   y asigna este objeto a cli
                    cli = new Cliente(dni, nombre, direccion);
                    //** 8 utiliza el método add del ArrayList para agregar cli a laLista
                    laLista.Add(cli);
                }

                if (laLista.Count > 0)
                {
                    todosCli = (Cliente[])laLista.ToArray(typeof(Cliente));
                }
                else
                {
                    todosCli = null;
                }
                return todosCli;
            }
            catch (SqlException e)
            {
                todosCli = null;
                Console.WriteLine(e.StackTrace);
                throw new ExcepcionVentas("ImplementacionDAOVentasCS.GetTodosLosClientes\n" + e.Message);
            }
            finally
            {
                // Cerrar la conexión
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public bool ExisteElDni(string dni)
        {
            SqlDataReader rdr = null;
            try
            {
                // Abrir la conexión
                laConexion.Open();

                // Crear una instancia de un nuevo comando con la conexión y una consulta
                SqlCommand cmd = new SqlCommand("SELECT DNI FROM Cliente WHERE DNI=" + "'" + dni + "'", laConexion);

                // Llamar al método que ejecuta el lector para obtener los resultados
                rdr = cmd.ExecuteReader();
                if (!rdr.HasRows) return false;
            }
            catch (SqlException e)
            {
                Console.WriteLine("En existeDNI, la consulta para " + dni + " falló");
                Console.WriteLine(e.StackTrace);
                return false;
            }
            finally
            {
                // cerrar el lector
                if (rdr != null)
                {
                    rdr.Close();
                }

                // cerrar la conexión
                if (laConexion != null)
                {
                    laConexion.Close();
                }
            }
            return true;
        }

        #endregion

        private ExcepcionClienteRemota GeneraInformacionExcepcionClienteRemota(string mensaje, string id,
            string direccion, string nombre)
        {
            ExcepcionClienteRemota informacion = new ExcepcionClienteRemota();
            informacion.MensajeDeError = mensaje;
            informacion.Dni = id;
            informacion.Dir = direccion;
            informacion.Nombre = nombre;
            return informacion;
        }
    }
}
