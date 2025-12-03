using Microsoft.EntityFrameworkCore;
using OpinionesAnalytics.Application.Dtos;
using OpinionesAnalytics.Application.Repositories;
using OpinionesAnalytics.Application.Result;
using OpinionesAnalytics.Domain.Csv;
using OpinionesAnalytics.Domain.Dwh.Dimensiones;
using OpinionesAnalytics.Domain.Repository;
using OpinionesAnalytics.Persistence.Dwh.Context;
using SWCE.Infraestructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Persistence.Dwh
{
    public class DwhRepositories : IDwhRepository
    {
        private readonly DWHOpinionesContext _dwhContext;
        private readonly IFileReaderRepository<Clientes> _clientesReader;
        private readonly IFileReaderRepository<Productos> _productosReader;
        private readonly IFileReaderRepository<surveys> _surveysReader;
        private readonly ILoggerBase<DwhRepositories> _logger;

        public DwhRepositories(
            DWHOpinionesContext dwhContext,
            IFileReaderRepository<Clientes> clientesReader,
            IFileReaderRepository<Productos> productosReader,
            IFileReaderRepository<surveys> surveysReader,
            ILoggerBase<DwhRepositories> logger)
        {
            _dwhContext = dwhContext;
            _clientesReader = clientesReader;
            _productosReader = productosReader;
            _surveysReader = surveysReader;
            _logger = logger;
        }

        public async Task<ServicesResult> LoadDimsDataAsync(DimDtos dimDtos)
        {
            ServicesResult result = new ServicesResult();
            _logger.LogInformation("Iniciando carga de dimensiones en el DWH de Opiniones.");

            try
            {
                result = await CleanDimenssionTables();

                if (!result.IsSuccess)
                {
                    _logger.LogError(result.Message);
                    return result;
                }

                var rawclientsData = await _clientesReader.ReadFileAsync(dimDtos.ClientsCsvPath);
                var rawproductsData = await _productosReader.ReadFileAsync(dimDtos.ProductsCsvPath);
                var rawOpinionsData = await _surveysReader.ReadFileAsync(dimDtos.SurveysCsvPath);

                //Dimension fuente
                var fuente = rawOpinionsData
                    .Select(op => op.Fuente.Trim())
                    .Distinct()
                    .Where(f => !string.IsNullOrEmpty(f))
                    .Select(f => new DimFuente
                    {
                        Nombre_Fuente = f,
                        Tipo_Fuente = (f.Contains("API") || f.Contains("Social")) ? "Digital" : "Interna"
                    }).ToArray();

                await _dwhContext.Fuentes.AddRangeAsync(fuente);
                await _dwhContext.SaveChangesAsync();

                //Dimesion producto
                var products = rawproductsData
                    .Select(op => new { op.IdProducto, op.Nombre, op.Categoria })
                    .Distinct()
                    .Where(p => p.IdProducto > 0)
                    .Select(p => new DimProductos
                    {
                    Id_Producto_Original = p.IdProducto,
                    Nombre_Producto = p.Nombre,
                    Categoria_Producto = p.Categoria  
                    }).ToArray();

                await _dwhContext.Productos.AddRangeAsync(products);
                await _dwhContext.SaveChangesAsync();

                //Dimension cliente}
                var clients = rawclientsData
                    .Select(op => new { op.IdCliente, op.Nombre, op.Email })
                    .Distinct()
                    .Where(c => c.IdCliente > 0)
                    .Select(c => new DimCLientes
                    {
                        Id_Cliente_Original = c.IdCliente,
                        Nombre_Cliente = c.Nombre,  
                        Pais = "Desconocido", // Asignar valor predeterminado
                        Ciudad = "Desconocido", // Asignar valor predeterminado
                        Tipo_Cliente = "Desconocido", // Asignar valor predeterminado
                        Grupo_Edad = "Desconocido" // Asignar valor predeterminado

                    }).ToArray();

                await _dwhContext.Clientes.AddRangeAsync(clients);
                await _dwhContext.SaveChangesAsync();

                //Dimension fecha
                var datafecha = rawOpinionsData
                    .Select(fe => fe.Fecha)
                    .Distinct()
                    .Select(fe => new DimFecha
                    {
                        Fecha_Completa = fe.Date,
                        Anio = fe.Year,
                        Dia = fe.Day,
                        Mes = fe.Month,
                        // La lógica de Trimestre, Semana.
                        Trimestre = (fe.Month - 1) / 3 + 1,
                        Fecha_Key = Convert.ToInt64(fe.Date.ToString("yyyyMMdd")),
                    }).ToArray();

                await _dwhContext.Fechas.AddRangeAsync(datafecha);
                await _dwhContext.SaveChangesAsync();
 

                result.IsSuccess = true;
                result.Message = "Carga de Dimensiones completada exitosamente.";
   
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading DWH Dims data: {ex.Message}", ex);
                result.IsSuccess = false;
                result.Message = $"Error loading DWH Dims data: {ex.Message}";
            }

            return result;
        }

        private async Task<ServicesResult> CleanDimenssionTables()
        {
            ServicesResult result = new ServicesResult();

            try
            {
                // Limpieza de las dimensiones del DWH de Opiniones
                await _dwhContext.Opiniones.ExecuteDeleteAsync();  
                await _dwhContext.Clientes.ExecuteDeleteAsync();
                await _dwhContext.Productos.ExecuteDeleteAsync();
                await _dwhContext.Fuentes.ExecuteDeleteAsync();
                await _dwhContext.Fechas.ExecuteDeleteAsync();
                 

                result = new ServicesResult() { IsSuccess = true, Message = "La data de las dimensiones fueron limpiadas." };
            }
            catch (Exception ex)
            {
                result = new ServicesResult()
                {
                    IsSuccess = false,
                    Message = $"Error limpiando las tablas de dimensiones: {ex.Message}"
                };
            }

            return result;
        }
    }
}
