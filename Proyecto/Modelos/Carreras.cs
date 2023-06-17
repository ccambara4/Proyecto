using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Modelos
{
    public class Carreras
    {
        public string ID_CARRERA { get; set; }
        public string NOMBRE { get; set; }
        public string HORARIO { get; set; }
        public string DURACION { get; set; }
        public string ID_MAESTRO { get; set; }
        public string ID_ESTABLECIMIENTO { get; set; }
        public string MAESTRO { get; set; }
        public string ESTABLECIMIENTO { get; set; }
    }
}