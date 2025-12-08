using Microsoft.Extensions.Configuration;
using OpinionesAnalytics.Application.Dtos;
using OpinionesAnalytics.Application.Interfaces;
using OpinionesAnalytics.Application.Repositories;
using OpinionesAnalytics.Application.Result;
using OpinionesAnalytics.Domain.Csv;
using OpinionesAnalytics.Domain.Repository;
using OpinionesAnalytics.Infrastructure.Logging;
using OpinionesAnalyticsAPI.DATA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Application.Services
{
    public class OpinionHandlerServices : IOpinionesHandlerServices
    {
        private readonly IDwhRepository _dwhRepository; 
        private readonly IConfiguration _configuration;
        private readonly ILoggerBase<OpinionHandlerServices> _logger;

        public OpinionHandlerServices(
            IDwhRepository dwhRepository, 
            IConfiguration configuration,
            ILoggerBase<OpinionHandlerServices> logger)
        {
            _dwhRepository = dwhRepository; 
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ServicesResult> ProcessOpinionesDataAsync()
        {
            ServicesResult serviceResult = new ServicesResult();
            try
            {
                _logger.LogInformation("Iniciando el procesamiento de datos de opiniones.");



                serviceResult.IsSuccess = true;
                serviceResult.Message = "Procesamiento completado exitosamente.";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al procesar los datos de opiniones: {ex.Message}");
                serviceResult.IsSuccess = false;
                serviceResult.Message = $"Error durante el procesamiento: {ex.Message}";
            }
            return serviceResult;
        }
    }
}
