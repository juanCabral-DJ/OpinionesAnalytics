using OpinionesAnalytics.Application.Interfaces;
using OpinionesAnalytics.Application.Repositories;
using OpinionesAnalytics.Domain.Csv;
using OpinionesAnalytics.Domain.Repository;
using OpinionesAnalytics.Persistence.Dwh.Context;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _provider;

        public Worker(ILogger<Worker> logger, IServiceProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    try
                    {

                        using (var scope = _provider.CreateScope())
                        {
                            IOpinionesHandlerServices opinionesHandlerServices = GetServices(scope);

                            await opinionesHandlerServices.ProcessOpinionesDataAsync();

                            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                            await Task.Delay(1000, stoppingToken);
                        }

                    }
                    catch (Exception)
                    {

                        _logger.LogError("Error procesando los datos.");
                    } 
                }
               
            }
        }

        private static IOpinionesHandlerServices GetServices(IServiceScope scope)
        {
            var db = scope.ServiceProvider.GetRequiredService<DWHOpinionesContext>(); 
            var dwhRepo = scope.ServiceProvider.GetRequiredService<IDwhRepository>();
            var inventoryHandlerService = scope.ServiceProvider.GetRequiredService<IOpinionesHandlerServices>();

            return inventoryHandlerService;
        }
    }
}
