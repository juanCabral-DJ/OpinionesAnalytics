using Microsoft.Extensions.Logging;
 

namespace SWCE.Infraestructure.Logging
{
    public class LoggerBase<T> : ILoggerBase<T> where T : class
    {
        public readonly ILogger<T> _Logger;

        public LoggerBase()
        {

        }
        public LoggerBase(ILogger<T> logger)
        {
              _Logger = logger;
        }
        public void LogError(string mensaje, Exception ex)
        {
            _Logger.LogError(mensaje, ex);
        }
        public void LogError(string mensaje)
        {
            _Logger.LogError(mensaje);
        }
        public void LogInformation(string mensaje, Object e)
        {
            _Logger.LogInformation(mensaje, e);
        }
        public void LogInformation(string mensaje)
        {
            _Logger.LogInformation(mensaje);
        }
    }
}
