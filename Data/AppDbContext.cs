using Microsoft.EntityFrameworkCore;
using SecureSeat.Models;

namespace SecureSeat.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Show> Shows { get; set; }
    }
    
    
}
