using Microsoft.EntityFrameworkCore;
using OpinionesAnalyticsAPI.DATA.Domain;
using System.Data;

namespace OpinionesAnalyticsAPI.DATA.Context
{
    public class Social_CommentsContext : DbContext
    {
        public Social_CommentsContext(DbContextOptions<Social_CommentsContext> options) : base(options) 
        { 
        }

        public DbSet<Social_Comments> Social_Comments { get; set; }

    }
}
