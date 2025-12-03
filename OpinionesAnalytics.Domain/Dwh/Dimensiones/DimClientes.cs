using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Domain.Dwh.Dimensiones
{
    public class DimCLientes
    {
        public long Cliente_Key { get; set; }  
        public int Id_Cliente_Original { get; set; }  
        public string Nombre_Cliente { get; set; }  
        public string Pais { get; set; } 
        public string Ciudad { get; set; }  
        public string Tipo_Cliente { get; set; }  
        public string Grupo_Edad { get; set; }  
    }
}
