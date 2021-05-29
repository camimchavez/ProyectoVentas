using System;
using System.ServiceModel;
using ServicioVentas;

namespace ClienteDeRed
{
    public class ClienteDelServicioVentas
    {
        private EndpointAddress direccionPuntoFinal;
        private WSHttpBinding enlace;
        private IContratoDelServicioVentas proxy;

        public ClienteDelServicioVentas()
        {

            direccionPuntoFinal = new EndpointAddress("http://localhost:8888/ServicioVentas");
            enlace = new WSHttpBinding();
            proxy = ChannelFactory<IContratoDelServicioVentas>.CreateChannel(enlace, direccionPuntoFinal);
        }

        public IContratoDelServicioVentas Proxy
        {
            get { return proxy; }
            set { proxy = value; }
        }
    }
}
