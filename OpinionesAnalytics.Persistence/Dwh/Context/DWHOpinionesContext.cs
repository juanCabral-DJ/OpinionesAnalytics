using Microsoft.EntityFrameworkCore;
using OpinionesAnalytics.Domain.Dwh.Dimensiones;
using OpinionesAnalytics.Domain.Dwh.Facts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Persistence.Dwh.Context
{
    public class DWHOpinionesContext : DbContext
    {
        public DWHOpinionesContext(DbContextOptions<DWHOpinionesContext> options) : base(options)
        {
        }

        //Dimensiones
        public DbSet<DimCLientes> Dim_Cliente { get; set; }
        public DbSet<DimProductos> Dim_Producto { get; set; }
        public DbSet<DimFuente> Dim_Fuente { get; set; }
        public DbSet<DimFecha> Dim_Fecha { get; set; }

        //Facts Table
        public DbSet<FactsOpiniones> Fact_Opiniones { get; set; }

    }
}
