using Microsoft.Extensions.Configuration;
using OpinionesAnalytics.Application.Dtos;
using OpinionesAnalytics.Application.Interfaces;
using OpinionesAnalytics.Application.Repositories;
using OpinionesAnalytics.Application.Result;
using OpinionesAnalytics.Domain.Csv;
using OpinionesAnalyticsAPI.DATA.Domain;
using SWCE.Infraestructure.Logging;
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
        private readonly IOpinionExtractor<WebReviews> _reviewsRepository;
        private readonly IOpinionExtractor<Social_Comments> _socialCommentsRepository;
        private readonly IOpinionExtractor<surveys> _surveysRepository;
        private readonly IConfiguration _configuration;
        private readonly ILoggerBase<OpinionHandlerServices> _logger;

        public OpinionHandlerServices(
            IDwhRepository dwhRepository,
            IOpinionExtractor<WebReviews> reviewsRepository,
            IOpinionExtractor<Social_Comments> socialCommentsRepository,
            IOpinionExtractor<surveys> surveysRepository, 
            IConfiguration configuration,
            ILoggerBase<OpinionHandlerServices> logger)
        {
            _dwhRepository = dwhRepository;
            _reviewsRepository = reviewsRepository;
            _socialCommentsRepository = socialCommentsRepository;
            _surveysRepository = surveysRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ServicesResult> ProcessOpinionesDataAsync()
        {
            ServicesResult serviceResult = new ServicesResult();
            try
            {
                _logger.LogInformation("Iniciando el procesamiento de datos de opiniones.");

                DimDtos dimDtos = new DimDtos();

                dimDtos.SurveysCsvPath = _configuration["ExternalSources:SurveysCsvPath"];
                dimDtos.ClientsCsvPath = _configuration["ExternalSources:ClienteCsv"];
                dimDtos.ProductsCsvPath = _configuration["ExternalSources:ProductoCsv"];

                serviceResult = await _dwhRepository.LoadDimsDataAsync(dimDtos);

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
