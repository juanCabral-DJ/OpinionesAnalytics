using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpinionesAnalyticsAPI.DATA.Context;
using OpinionesAnalyticsAPI.DATA.Domain;
using OpinionesAnalyticsAPI.DATA.Interface;
using OpinionesAnalyticsAPI.DATA.Logging;
using System.Net;

namespace OpinionesAnalyticsAPI.DATA.Persistence
{
    public class WebReviewsRepository : IWebReviewsRepository
    {
        private readonly ILoggerBase<WebReviews> _logger;
        private readonly WebReviewsContext _Context;
        public WebReviewsRepository(WebReviewsContext _context, ILoggerBase<WebReviews> logger) : base()
        {
            _Context = _context;
            _logger = logger;
        }
        public async Task<OperationResult> GetReviewsAsync()
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving webReviews entities");
                var webReviews = await _Context.WebReviews.ToListAsync();

                result = OperationResult.Success("Retrieving webReviews entities", webReviews);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving webReviews entities", e);
                result = OperationResult.Failure("An error occurred while retrieving webReviews entities.");
            }

            return result;
        }
    }
}
