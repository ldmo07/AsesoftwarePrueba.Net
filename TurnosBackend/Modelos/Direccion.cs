using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
   public class Direccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_direccion { get; set; }
        public string pais { get; set; }
        public string ciudad { get; set; }

        public string barrio { get; set; }

        public string calle { get; set; }
    }
}
