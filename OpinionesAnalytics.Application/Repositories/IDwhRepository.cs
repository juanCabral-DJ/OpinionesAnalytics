using Microsoft.VisualBasic;
using OpinionesAnalytics.Application.Dtos;
using OpinionesAnalytics.Application.Result;
using OpinionesAnalytics.Domain.Dwh.Dimensiones;
using OpinionesAnalytics.Domain.Dwh.Facts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Application.Repositories
{
        public interface IDwhRepository
        {
            Task<ServicesResult> CleanDimenssionTables();

            
            Task LoadClientesBulkAsync(IEnumerable<DimCLientes> clientes);
            Task LoadProductosBulkAsync(IEnumerable<DimProductos> productos);
            Task LoadFuentesBulkAsync(IEnumerable<DimFuente> fuentes);
            Task LoadFechaBulkAsync(IEnumerable<DimFecha> fecha);
            Task LoadFactsBulkAsync(IEnumerable<FactsOpiniones> facts);
             
            Task<List<DimCLientes>> GetclientesAsync();
            Task<List<DimProductos>> GetProductosAsync();
            Task<List<DimFuente>> GetFuentesAsync();
            Task<List<DimFecha>> GetFechaAsync();
    }
    }


