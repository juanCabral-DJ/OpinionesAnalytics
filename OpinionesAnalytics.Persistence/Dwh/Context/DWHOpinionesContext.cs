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
        public DbSet<DimCLientes> Clientes { get; set; }
        public DbSet<DimProductos> Productos { get; set; }
        public DbSet<DimFuente> Fuentes { get; set; }
        public DbSet<DimFecha> Fechas { get; set; }

        //Facts Table
        public DbSet<FactsOpiniones> Opiniones { get; set; }

    }
}
