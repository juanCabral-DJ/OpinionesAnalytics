using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Domain.Dwh.Dimensiones
{
    public class DimFecha
    {
        public long Fecha_Key { get; set; }  
        public DateTime? Fecha_Completa { get; set; }  
        public int? Mes { get; set; }  
        public int? Dia { get; set; }  
        public int? Trimestre { get; set; }  
        public int? Anio { get; set; }  
    }
}
