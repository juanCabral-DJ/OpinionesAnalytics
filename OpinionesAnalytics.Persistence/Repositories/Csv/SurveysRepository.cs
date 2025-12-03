 
using Microsoft.Extensions.Configuration;
using OpinionesAnalytics.Application.Repositories;
using OpinionesAnalytics.Domain.Csv;
using OpinionesAnalytics.Domain.Csv.Base;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Persistence.Repositories.Csv
{
    public class SurveysRepository : IOpinionExtractor<surveys>
    {
         
        public readonly ILoggerBase<SurveysRepository> _Logger;
        public readonly string filepath; 

        public SurveysRepository(ILoggerBase<SurveysRepository> logger, IConfiguration config)
        {
            _Logger = logger;
            this.filepath = config["ExternalSources:surveysCsv"];
        }

        public async Task<IEnumerable<surveys>> ExtractAsync(string filepath = "")
        {
            var survey = new List<surveys>();
            try
            {
                _Logger.LogInformation("Extrayendo los datos desde el csv");
                survey = await CsvReaderBase.LeerCsv<surveys>(filepath);

            }
            catch (Exception ex)
            {
                _Logger.LogError("han ocurrido errores al extraer los datos", ex);
            }

            return survey;
        }
    }
}
