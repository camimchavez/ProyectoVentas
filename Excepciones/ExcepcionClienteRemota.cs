using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Excepciones
{
    [DataContract]
    public class ExcepcionClienteRemota
    {
        private string _mensajeDeError = "No Definido";

        [DataMember]
        public string MensajeDeError
        {
            get { return _mensajeDeError; }
            set { _mensajeDeError = value; }
        }

        private String _dni;

        [DataMember]
        public String Dni
        {
            get { return _dni; }
            set { _dni = value; }
        }

        private String _nombre;

        [DataMember]
        public String Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private String _dir;

        [DataMember]
        public String Dir
        {
            get { return _dir; }
            set { _dir = value; }
        }
    }
}
