using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Application.Dtos
{
    public class Social_CommentsDto
    {
        public string? IdComment { get; set; } 
        public int IdCliente { get; set; }
         
        public int IdProducto { get; set; }
         
        public string? Fuente { get; set; }
         
        public DateTime Fecha { get; set; }
         
        public string? Comentario { get; set; }
    }
}
