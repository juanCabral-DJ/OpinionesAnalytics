using Microsoft.EntityFrameworkCore;
using OpinionesAnalytics.Application.Repositories;
using OpinionesAnalytics.Domain.Csv;
using OpinionesAnalytics.Infrastructure.Logging;
using OpinionesAnalytics.Persistence.Repositories.Db.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Persistence.Repositories.Db
{
    public class ReviewsRepository : IOpinionExtractor<WebReviews>
    {
        private readonly ILoggerBase<WebReviews> _logger;
        public readonly ReviewsContext _context;
        public ReviewsRepository(ReviewsContext context, ILoggerBase<WebReviews> logger)  : base()
        {
            this._context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<WebReviews>> ExtractAsync()
        {
            var reviews = new List<WebReviews>();
            try
            {
                _logger.LogInformation("Extrayendo los datos de las reseñas desde la base de datos");
                reviews =  await _context.WebReviews.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Ha ocurrido algun error al querer extraer los datos de las reseñas", ex);
            }
            finally
            {
            }

            return reviews;
        }

        
    }
}
