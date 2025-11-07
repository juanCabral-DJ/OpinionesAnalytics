using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Domain.Csv
{
    public class WebReviews
    {
        public string IdReview { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public int Rating { get; set; }
    }
}
 