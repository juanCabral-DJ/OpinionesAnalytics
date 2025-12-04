
using Microsoft.Extensions.Configuration;
using OpinionesAnalytics.Application.Repositories;
using OpinionesAnalytics.Domain.Csv;
using OpinionesAnalytics.Domain.Csv.Base;
using OpinionesAnalytics.Domain.Repository;
using OpinionesAnalytics.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Persistence.Repositories.Csv
{
    public class SurveysRepository : IFileReaderRepository<surveys>
    {
        private readonly ILoggerBase<SurveysRepository> _Logger;
        private readonly IConfiguration _configuration;
        private readonly string filepath;

        public SurveysRepository(ILoggerBase<SurveysRepository> logger, IConfiguration config)
        {
            _Logger = logger;
            this.filepath = config["ExternalSources:surveysCsv"];
        }

        public async Task<IEnumerable<surveys>> ReadFileAsync(string filePath)
        {
            var surveys = new List<surveys>();
            _Logger.LogInformation("Reading CSV file at path: {FilePath}", filePath);

            try
            {

                surveys = await CsvReaderBase.LeerCsv<surveys>(filepath);

            }
            catch (Exception ex)
            {
                surveys = null;
                _Logger.LogError("han ocurrido errores al extraer los datos", ex);
            }

            return surveys;

        }
    }
}
