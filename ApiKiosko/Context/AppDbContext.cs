using ApiKiosko.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiKiosko.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Clientes> Clientes { get; set;}
        public DbSet<Ventas> Ventas { get; set; }
    }
}
