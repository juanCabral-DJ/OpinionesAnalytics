using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using OpinionesAnalytics.Application.Dtos;
using OpinionesAnalytics.Application.Repositories;
using OpinionesAnalytics.Application.Result;
using OpinionesAnalytics.Domain.Csv;
using OpinionesAnalytics.Domain.Dwh.Dimensiones;
using OpinionesAnalytics.Domain.Dwh.Facts;
using OpinionesAnalytics.Domain.Repository;
using OpinionesAnalytics.Infrastructure.Logging;
using OpinionesAnalytics.Persistence.Dwh.Base;
using OpinionesAnalytics.Persistence.Dwh.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Persistence.Dwh
{
    public class DwhRepositories : IDwhRepository
    {
        private readonly DWHOpinionesContext _dwhContext;
        private readonly ILoggerBase<DwhRepositories> _logger;
        private readonly string _connectionString;

        public DwhRepositories(IConfiguration configuration,
            DWHOpinionesContext dwhContext,
            ILoggerBase<DwhRepositories> logger)
        {
            _connectionString = configuration["DWHConnectionString:DWHDB"];
            _dwhContext = dwhContext;
            _logger = logger;
        }

        private async Task BulkInsertAsync<T>(IEnumerable<T> data, string tableName)
        {
            _logger.LogInformation("creando el metodo bulk insert para insertar a la bd");

            var list = data.ToList();
            if (!list.Any()) return;
            var dataTable = list.DataTable();
            using (var bulkCopy = new SqlBulkCopy(_connectionString))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BatchSize = 60000;
                bulkCopy.BulkCopyTimeout = 700;
                foreach (DataColumn column in dataTable.Columns)
                    bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                await bulkCopy.WriteToServerAsync(dataTable);
            }
        }

        public async Task LoadClientesBulkAsync(IEnumerable<DimCLientes> c)
        {
            _logger.LogInformation($"Load clientes bulk");
            await BulkInsertAsync(c, "Dimension.Dim_Cliente");
        }
        public async Task LoadProductosBulkAsync(IEnumerable<DimProductos> p)
        {
            _logger.LogInformation($"Load productos bulk");
            await BulkInsertAsync(p, "Dimension.Dim_Producto");
        }
        public async Task LoadFechaBulkAsync(IEnumerable<DimFecha> fe)
        {
            _logger.LogInformation($"Load fechas bulk");
            await BulkInsertAsync(fe, "Dimension.Dim_Fecha");
        }
        public async Task LoadFuentesBulkAsync(IEnumerable<DimFuente> fu)
        {
            _logger.LogInformation($"Load fuentes bulk");
            await BulkInsertAsync(fu, "Dimension.Dim_Fuente");
        }
        public async Task LoadFactsBulkAsync(IEnumerable<FactsOpiniones> f)
        {
            _logger.LogInformation($"Load fact table bulk");
            await BulkInsertAsync(f, "Fact.Fact_Opiniones");
        }

        // Lecturas  
        public async Task<List<DimCLientes>> GetclientesAsync() => await _dwhContext.Dim_Cliente.AsNoTracking().ToListAsync();
        public async Task<List<DimProductos>> GetProductosAsync() => await _dwhContext.Dim_Producto.AsNoTracking().ToListAsync();
        public async Task<List<DimFuente>> GetFuentesAsync() => await _dwhContext.Dim_Fuente.AsNoTracking().ToListAsync();
        public async Task<List<DimFecha>> GetFechaAsync() => await _dwhContext.Dim_Fecha.AsNoTracking().ToListAsync();


        public async Task<ServicesResult> CleanDimenssionTables()
        {
            ServicesResult result = new ServicesResult();

            try
            {
                _logger.LogInformation("Limoiando las tables de la bd");

                // Limpieza de las dimensiones del DWH de Opiniones
                await _dwhContext.Fact_Opiniones.ExecuteDeleteAsync();
                await _dwhContext.Dim_Cliente.ExecuteDeleteAsync();
                await _dwhContext.Dim_Producto.ExecuteDeleteAsync();
                await _dwhContext.Dim_Fuente.ExecuteDeleteAsync();
                await _dwhContext.Dim_Fecha.ExecuteDeleteAsync();


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
