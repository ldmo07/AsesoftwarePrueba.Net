using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Servicio
    {
        public int id_servicio { get; set; }
        public int id_comercio { get; set; }
        public string nom_servicio { get; set; }
        public TimeSpan hora_apertura { get; set; }
        public TimeSpan hora_cierre { get; set; }
        public TimeSpan duracion { get; set; }
    }
}
