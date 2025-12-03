using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Domain.Dwh.Dimensiones
{
    public class DimFuente
    {
        public long Fuente_Key { get; set; }  
        public string Nombre_Fuente { get; set; }  
        public string Tipo_Fuente { get; set; }  
    }
}
