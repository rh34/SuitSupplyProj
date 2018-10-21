using Microsoft.EntityFrameworkCore;
using SS.Entities.Data;

namespace SS.Repositories
{
    public class SuitsupplyDbContext : DbContext
    {
        public SuitsupplyDbContext(DbContextOptions<SuitsupplyDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
