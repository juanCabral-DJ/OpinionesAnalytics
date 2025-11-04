 

namespace SWCE.Infraestructure.Logging
{
    public interface ILoggerBase<TEntity> where TEntity : class
    {
        void LogInformation(string mensaje, Object entity);
        void LogError(string mensaje, Exception ex);
        void LogError(string mensaje);
        void LogInformation(string mensaje);
    }
}
