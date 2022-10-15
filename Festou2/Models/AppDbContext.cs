using Festou2.Models;
using Microsoft.EntityFrameworkCore;


namespace Festou2.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Locador> Locador { get; set; }
        public DbSet<Local> Local { get; set; }
    }
}
