using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Domain.Dwh.Facts
{
    [Table("Fact_Opiniones", Schema = "Fact")]
    public class FactsOpiniones
    {
        [Key]
        public long Id_Opinion { get; set; }  

        // Claves Foráneas  
        public long Cliente_Key { get; set; }
        public long Producto_Key { get; set; }
        public long Fecha_Key { get; set; }
        public long Fuente_Key { get; set; }

        // Métricas  
        public int? Calificacion { get; set; }  
        public int? Sentimiento_Clasificado { get; set; }  
        public int? Contador_Opinion { get; set; }  
    }
}
