using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Domain.Dwh.Dimensiones
{
    [Table("Dim_Fecha", Schema = "Dimension")]
    public class DimFecha
    {
        [Key]
        public long Fecha_Key { get; set; }  
        public DateTime? Fecha_Completa { get; set; }  
        public int? Mes { get; set; }  
        public int? Dia { get; set; }  
        public int? Trimestre { get; set; }  
        public int? Año { get; set; }  
    }
}
