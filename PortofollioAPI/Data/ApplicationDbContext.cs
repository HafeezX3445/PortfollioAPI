using Microsoft.EntityFrameworkCore;
using PortofollioAPI.Models;

namespace PortofollioAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
