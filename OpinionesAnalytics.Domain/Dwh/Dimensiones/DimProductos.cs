using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Domain.Dwh.Dimensiones
{
    public class DimProductos
    {
        public long Producto_Key { get; set; }  
        public int Id_Producto_Original { get; set; }  
        public string Nombre_Producto { get; set; }  
        public string Categoria_Producto { get; set; }  
    }
}
