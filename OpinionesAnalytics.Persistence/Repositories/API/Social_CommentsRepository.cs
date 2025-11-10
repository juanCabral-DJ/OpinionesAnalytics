using Microsoft.Extensions.Configuration;
using OpinionesAnalytics.Application.Repositories;
using OpinionesAnalyticsAPI.DATA.Domain;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Persistence.Repositories.API
{
    public class Social_CommentsRepository : IOpinionExtractor<Social_Comments>
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerBase<Social_CommentsRepository> _logger;
        private readonly string _apiUrl;

        public Social_CommentsRepository(HttpClient httpClient, ILoggerBase<Social_CommentsRepository> logger, IConfiguration config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiUrl = config["ExternalSources:Social_CommentsApi"];
        }

        public async Task<IEnumerable<Social_Comments>> ExtractAsync(CancellationToken cancellationToken = default)
        {
            var comments = new List<Social_Comments>();
            try
            {
                _logger.LogInformation("Extrayendo los datos desde la API");
                comments = await _httpClient.GetFromJsonAsync<List<Social_Comments>>(_apiUrl, cancellationToken);

            }catch (Exception ex)
            {
                _logger.LogError("Ha ocurrido un error al extraer los datos desde la API", ex);
                comments = new List<Social_Comments>();
            }
            return comments;
        }
    }
}
