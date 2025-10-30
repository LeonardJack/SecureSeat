using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using SecureSeat.Models;

namespace SecureSeat.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet for Shows
        public DbSet<Show> Shows { get; set; }

        // DbSet for Users
        public DbSet<User> Users { get; set; }
    }
    
    
}
