using Microsoft.EntityFrameworkCore;
using OpinionesAnalyticsAPI.DATA.Domain;
using System.Data;

namespace OpinionesAnalyticsAPI.DATA.Context
{
    public class WebReviewsContext : DbContext
    {
        public WebReviewsContext(DbContextOptions<WebReviewsContext> options) : base(options) 
        { 
        }

        public DbSet<WebReviews> WebReviews { get; set; }

    }
}
