using System.ComponentModel.DataAnnotations;

namespace OpinionesAnalyticsAPI.DATA.Domain
{
    public class WebReviews
    {
        [Key]
        public int IdReview { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public int Rating { get; set; }
    }
}
