using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Domain.Dwh.Dimensiones
{
    [Table("Dim_Fuente", Schema = "Dimension")]
    public class DimFuente
    {
        [Key]
        public long Fuente_Key { get; set; }  
        public string Nombre_Fuente { get; set; }  
        public string Tipo_Fuente { get; set; }  
    }
}
