using Finance.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Finance.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Transaction> Transacitions { get; set; }= null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new CategoryMapping());
            // modelBuilder.ApplyConfiguration(new TransactionMapping());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
