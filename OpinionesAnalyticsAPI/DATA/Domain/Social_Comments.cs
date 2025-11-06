using System.ComponentModel.DataAnnotations;

namespace OpinionesAnalyticsAPI.DATA.Domain
{
    public class Social_Comments
    {
        [Key]
        public int IdComment { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public string Fuente { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
    }
}
