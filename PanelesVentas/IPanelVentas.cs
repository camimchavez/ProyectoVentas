using System;
using Ventas;
using ServicioVentas;

namespace Paneles
{
    public interface IPanelVentas
    {
        void RegistrarControlador(IControladorVentas controlador);
//        void RegistrarModelo(IModeloVentas modelo);
        void RegistrarModelo(IContratoDelServicioVentas modelo);
        void MostrarPorPantalla(Object obj);
        void Refrescar();
        
    }
}
