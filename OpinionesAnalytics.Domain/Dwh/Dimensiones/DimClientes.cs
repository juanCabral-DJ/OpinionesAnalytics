using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Domain.Dwh.Dimensiones
{
    [Table("Dim_Cliente", Schema = "Dimension")]
    public class DimCLientes
    {
        [Key]
        public long Cliente_Key { get; set; }  
        public int Id_Cliente_Original { get; set; }  
        public string Nombre_Cliente { get; set; }
        public string Pais { get; set; } = null;
        public string Ciudad { get; set; } = null;
        public string Tipo_Cliente { get; set; } = null;
        public string Grupo_Edad { get; set; } = null;
    }
}
