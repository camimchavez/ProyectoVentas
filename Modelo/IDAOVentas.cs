using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ventas;

namespace Modelo
{
    public interface IDAOVentas
    {
        void CrearCliente(Cliente cliente);
        void RemoverCliente(Cliente cliente);
        void ModificarCliente(Cliente cliente);
        Cliente GetCliente(String dni);
        Cliente[] GetTodosLosClientes();
        bool ExisteElDni(String dni);
    }
}
