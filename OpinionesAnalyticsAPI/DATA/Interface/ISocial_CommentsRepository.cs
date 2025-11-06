using OpinionesAnalyticsAPI.DATA.Domain;

namespace OpinionesAnalyticsAPI.DATA.Interface
{
    public interface ISocial_CommentsRepository
    {
        public Task<OperationResult> GetSocial_CommentsAsync();
    }
}
