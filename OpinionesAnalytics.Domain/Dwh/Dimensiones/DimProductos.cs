using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Domain.Dwh.Dimensiones
{
    [Table("Dim_Producto", Schema = "Dimension")]
    public class DimProductos
    {
        [Key]
        public long Producto_Key { get; set; }  
        public int Id_Producto_Original { get; set; }  
        public string nombre_producto { get; set; }  
        public string categoria_producto { get; set; }  
    }
}
