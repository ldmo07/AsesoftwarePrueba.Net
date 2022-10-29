using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
   public  class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_cliente { get; set; }
        public string nom_cliente { get; set; }
        public string ape_cliente { get; set; }
        public string nid_cliente { get; set; }

        public int id_direccion { get; set; }

        [ForeignKey("id_direccion")]
        public virtual Direccion Direccion { get; set; }
    }
}
