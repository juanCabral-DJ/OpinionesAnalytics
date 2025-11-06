using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpinionesAnalyticsAPI.DATA.Context;
using OpinionesAnalyticsAPI.DATA.Domain;
using OpinionesAnalyticsAPI.DATA.Interface;
using OpinionesAnalyticsAPI.DATA.Logging;
using System.Net;

namespace OpinionesAnalyticsAPI.DATA.Persistence
{
    public class Social_CommentsRepository : ISocial_CommentsRepository
    {
        private readonly ILoggerBase<Social_Comments> _logger;
        private readonly Social_CommentsContext _Context;
        public Social_CommentsRepository(Social_CommentsContext _context, ILoggerBase<Social_Comments> logger) : base()
        {
            _Context = _context;
            _logger = logger;
        }
        public async Task<OperationResult> GetSocial_CommentsAsync()
        {
            OperationResult result = new OperationResult();

            try
            {
                _logger.LogInformation("Retrieving Social_Comments entities");
                var webReviews = await _Context.Social_Comments.ToListAsync();

                result = OperationResult.Success("Retrieving Social_Comments entities", webReviews);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving Social_Comments entities", e);
                result = OperationResult.Failure("An error occurred while retrieving Social_Comments entities.");
            }

            return result;
        }
    }
}
