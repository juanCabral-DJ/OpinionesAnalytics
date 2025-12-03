using Microsoft.Extensions.Configuration;
using OpinionesAnalytics.Application.Repositories;
using OpinionesAnalytics.Domain.Csv;
using OpinionesAnalytics.Domain.Csv.Base;
using OpinionesAnalytics.Domain.Repository;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Persistence.Repositories.Csv
{
    public class ClienteRepository : IFileReaderRepository<Clientes>
    {
        private readonly ILoggerBase<ClienteRepository> _Logger;
        private readonly IConfiguration _configuration;
        private readonly string filepath;

        public ClienteRepository(ILoggerBase<ClienteRepository> logger, IConfiguration config)
        {
            _Logger = logger;
            this.filepath = config["ExternalSources:ClienteCsv"];
        }

        public async Task<IEnumerable<Clientes>> ReadFileAsync(string filePath)
        {
             var cliente = new List<Clientes>();
            _Logger.LogInformation("Reading CSV file at path: {FilePath}", filePath);

            try
            {

                cliente = await CsvReaderBase.LeerCsv<Clientes>(filepath);

            }
            catch (Exception ex)
            {
                cliente = null;
                _Logger.LogError("han ocurrido errores al extraer los datos", ex);
            }

            return cliente;

        }
    }
}
