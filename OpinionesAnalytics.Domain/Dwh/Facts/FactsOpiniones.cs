using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Domain.Dwh.Facts
{
    public class FactsOpiniones
    {
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
