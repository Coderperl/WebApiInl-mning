using Microsoft.EntityFrameworkCore;

namespace WebApiInlämning.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            
        }
        public DbSet<Advertisement> Advertisements { get; set; }
    }
}
