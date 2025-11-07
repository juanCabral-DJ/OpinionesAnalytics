using Microsoft.EntityFrameworkCore;
using OpinionesAnalytics.Domain.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Persistence.Repositories.Db.Context
{
    public class ReviewsContext : DbContext
    { 
        public ReviewsContext(DbContextOptions<ReviewsContext> options) : base(options)
        { 
        }

        public DbSet<WebReviews> WebReviews { get; set; }
    }
}
