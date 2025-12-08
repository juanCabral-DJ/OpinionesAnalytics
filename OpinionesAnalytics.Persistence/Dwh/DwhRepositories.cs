using Microsoft.EntityFrameworkCore;
using OpinionesAnalytics.Application.Dtos;
using OpinionesAnalytics.Application.Repositories;
using OpinionesAnalytics.Application.Result;
using OpinionesAnalytics.Domain.Csv;
using OpinionesAnalytics.Domain.Dwh.Dimensiones;
using OpinionesAnalytics.Domain.Repository;
using OpinionesAnalytics.Infrastructure.Logging;
using OpinionesAnalytics.Persistence.Dwh.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Persistence.Dwh
{
        private readonly DWHOpinionesContext _dwhContext;
        private readonly ILoggerBase<DwhRepositories> _logger;

        public DwhRepositories(
            DWHOpinionesContext dwhContext,
            ILoggerBase<DwhRepositories> logger)
        {
            _dwhContext = dwhContext;
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
        }

        private async Task<ServicesResult> CleanDimenssionTables()
        {
            ServicesResult result = new ServicesResult();

            try
            {
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
