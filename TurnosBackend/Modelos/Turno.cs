using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Turno
    {
      public int id_turno {get;set;}
      public int id_servicio {get;set;}
      public DateTime fecha_turno {get;set;}
      public TimeSpan hora_inicio {get;set;}
      public TimeSpan hora_fin {get;set;}
      public bool estado {get;set;}
    }
}
