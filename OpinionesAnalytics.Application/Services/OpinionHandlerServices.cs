using Microsoft.Extensions.Configuration;
using OpinionesAnalytics.Application.Dtos;
using OpinionesAnalytics.Application.Interfaces;
using OpinionesAnalytics.Application.Repositories;
using OpinionesAnalytics.Application.Result;
using OpinionesAnalytics.Domain.Csv;
using OpinionesAnalytics.Domain.Dwh.Dimensiones;
using OpinionesAnalytics.Domain.Dwh.Facts;
using OpinionesAnalytics.Domain.Repository;
using OpinionesAnalytics.Infrastructure.Logging;
using OpinionesAnalyticsAPI.DATA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Application.Services
{
    public class OpinionHandlerServices : IOpinionesHandlerServices
    {
        private readonly IDwhRepository _dwhRepository; 
        private readonly IConfiguration _configuration;
        private readonly IFileReaderRepository<Clientes> _clienteReader;
        private readonly IFileReaderRepository<Productos> _productoReader;
        private readonly IFileReaderRepository<surveys> _surveysReader; 
        private readonly ILoggerBase<OpinionHandlerServices> _logger;

        public OpinionHandlerServices(
            IFileReaderRepository<surveys> surveysReader,
            IFileReaderRepository<Productos> productoReader,
            IFileReaderRepository<Clientes> clienteReader, 
            IDwhRepository dwhRepository, 
            IConfiguration configuration,
            ILoggerBase<OpinionHandlerServices> logger)
        {
            _clienteReader = clienteReader;
            _productoReader = productoReader; 
            _surveysReader = surveysReader;
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

                var dimdtos = new DimDtos();

                var cliente = await _clienteReader.ReadFileAsync(dimdtos.ClientsCsvPath);
                var productos = await _productoReader.ReadFileAsync(dimdtos.ProductsCsvPath);
                var surveys = await _surveysReader.ReadFileAsync(dimdtos.SurveysCsvPath);

                _logger.LogInformation("iniciando la transformacion de los datos extraidos");

                await _dwhRepository.CleanDimenssionTables();

                //Dimension fecha

                var dimFecha = surveys.Select(fe => fe.Fecha)
                    .Distinct()
                    .Select(fe => new DimFecha
                    {
                        Fecha_Completa = fe.Date,
                        Año = fe.Year,
                        Dia = fe.Day,
                        Mes = fe.Month,
                        // La lógica de Trimestre 
                        Trimestre = (fe.Month - 1) / 3 + 1
                    }).ToArray();
                await _dwhRepository.LoadFechaBulkAsync(dimFecha);

                //Dimension clientes
                var dimcliente = cliente.Select(c => new { c.IdCliente, c.Nombre, c.Email })
                    .Distinct()
                    .Where(c => c.IdCliente > 0)
                    .Select(c => new DimCLientes
                    {
                        Id_Cliente_Original = c.IdCliente,
                        Nombre_Cliente = c.Nombre,
                        Pais = "Desconocido", // Asignar valor predeterminado
                        Ciudad = "Desconocido", // Asignar valor predeterminado
                        Tipo_Cliente = "Desconocido", // Asignar valor predeterminado
                        Grupo_Edad = "Null" // Asignar valor predeterminado

                    }).ToArray();
                await _dwhRepository.LoadClientesBulkAsync(dimcliente);

                //Dimension productos
                var dimproducto = productos.Select(op => new { op.IdProducto, op.Nombre, op.Categoría })
                    .Distinct()
                    .Where(p => p.IdProducto > 0)
                    .Select(p => new DimProductos
                    {
                        Id_Producto_Original = p.IdProducto,
                        nombre_producto = p.Nombre,
                        categoria_producto = p.Categoría
                    }).ToArray();
                await _dwhRepository.LoadProductosBulkAsync(dimproducto);

                //Dimension fuentes
                var dimfuentes = surveys.Select(op => op.Fuente.Trim())
                    .Distinct()
                    .Where(f => !string.IsNullOrEmpty(f))
                    .Select(f => new DimFuente
                    {
                        Nombre_Fuente = f,
                        Tipo_Fuente = (f.Contains("API") || f.Contains("Social")) ? "Digital" : "Interna"
                    }).ToArray();
                await _dwhRepository.LoadFuentesBulkAsync(dimfuentes);

                _logger.LogInformation("recuperando datos para factstable");

                var clientedb = await _dwhRepository.GetclientesAsync();
                var productodb = await _dwhRepository.GetProductosAsync();
                var fuentedb = await _dwhRepository.GetFuentesAsync();
                var fechadb = await _dwhRepository.GetFechaAsync();

                var clienteDic = clientedb.ToDictionary(c => c.Id_Cliente_Original, x => x.Cliente_Key);
                var productoeDic = productodb.ToDictionary(p => p.Id_Producto_Original, x => x.Producto_Key);
                var fuenteDic = fuentedb.ToDictionary(fu => fu.Nombre_Fuente, x => x.Fuente_Key);
                var fechaDic = fechadb.ToDictionary(fe => fe.Fecha_Completa, x => x.Fecha_Key);

                //Cargando fact table
                var fact = new List<FactsOpiniones>();

                foreach (var op in surveys)
                {
                     
                    // Fecha
                    long fechaKey =  fechadb
                        .Where(f => f.Fecha_Completa == op.Fecha)
                        .Select(f => f.Fecha_Key)
                        .FirstOrDefault();

                    // Cliente
                    long clienteKey = clienteDic.TryGetValue(op.IdCliente, out long ck) ? ck : -1;

                    // Producto
                    long productoKey = productoeDic.TryGetValue(op.IdProducto, out long pKey) ? pKey : -1;

                    // Fuente   
                    long fuenteKey = fuentedb
                        .Where(f => f.Nombre_Fuente.Trim() == op.Fuente.Trim())
                        .Select(f => f.Fuente_Key)
                        .FirstOrDefault();

                    
                    if (clienteKey != -1 && productoKey != -1 && fechaKey != 0 && fuenteKey != 0)
                    {
                        fact.Add(new FactsOpiniones
                        {
                            Fecha_Key = fechaKey,
                            Cliente_Key = clienteKey,
                            Producto_Key = productoKey,
                            Fuente_Key = fuenteKey,


                            // Medidas 
                            Sentimiento_Clasificado = ClassifyComment(op.Comentario),
                            Calificacion = op.PuntajeSatisfacción,
                            Contador_Opinion = 1
                        });
                    }
                }
                await _dwhRepository.LoadFactsBulkAsync(fact);


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

        private int ClassifyComment(string comentario)
        {
            if (string.IsNullOrWhiteSpace(comentario)) return 0;

            comentario = comentario.ToLower();

            string[] positives = { "bueno", "excelente", "genial", "me gusta", "perfecto", "super" };
            string[] negatives = { "malo", "terrible", "horrible", "pésimo", "no me gusta", "fatal" };

            int score = 0;

            if (positives.Any(p => comentario.Contains(p))) score++;
            if (negatives.Any(n => comentario.Contains(n))) score--;

            return score > 0 ? 1 : score < 0 ? -1 : 0;
        }
    }
}
