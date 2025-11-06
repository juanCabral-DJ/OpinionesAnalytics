using OpinionesAnalyticsAPI.DATA.Domain;

namespace OpinionesAnalyticsAPI.DATA.Interface
{
    public interface IWebReviewsRepository
    {
        public Task<OperationResult> GetReviewsAsync();
    }
}
