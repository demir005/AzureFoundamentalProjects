using AzureTangyFunc.Models;
using Microsoft.EntityFrameworkCore;


namespace AzureTangyFunc.Data
{
    public class AzureDbContext : DbContext
    {
        public AzureDbContext(DbContextOptions<AzureDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<SalesRequests> SalesRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SalesRequests>(entity =>
            {
                entity.HasKey(c => c.Id);
            });
        }
    }
}
