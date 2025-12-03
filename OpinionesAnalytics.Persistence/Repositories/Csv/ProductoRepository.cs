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
    public class ProductoRepository : IFileReaderRepository<Productos>
    {
        private readonly ILoggerBase<ProductoRepository> _Logger;
    private readonly IConfiguration _configuration;
    private readonly string filepath;

    public ProductoRepository(ILoggerBase<ProductoRepository> logger, IConfiguration config)
    {
        _Logger = logger;
        this.filepath = config["ExternalSources:ProductoCsv"];
    }

    public async Task<IEnumerable<Productos>> ReadFileAsync(string filePath)
    {
        var productos = new List<Productos>();
        _Logger.LogInformation("Reading CSV file at path: {FilePath}", filePath);

        try
        {

            productos = await CsvReaderBase.LeerCsv<Productos>(filepath);

        }
        catch (Exception ex)
        {
            productos = null;
            _Logger.LogError("han ocurrido errores al extraer los datos", ex);
        }

        return productos;

    }
}
}
