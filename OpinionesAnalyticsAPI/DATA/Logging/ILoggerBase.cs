namespace OpinionesAnalyticsAPI.DATA.Logging
{
    public interface ILoggerBase<TEntity> where TEntity : class
    {
        void LogInformation(string mensaje, object entity);
        void LogError(string mensaje, Exception ex);
        void LogError(string mensaje);
        void LogInformation(string mensaje);
    }
}
