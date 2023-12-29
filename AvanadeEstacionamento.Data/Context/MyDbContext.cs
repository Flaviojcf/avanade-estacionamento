using AvanadeEstacionamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AvanadeEstacionamento.Data.Context
{
    public class MyDbContext : DbContext
    {
        public DbSet<VeiculoModel> VeiculoModel { get; set; }
        public DbSet<EstacionamentoModel> EstacionamentoModel { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var properties = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(p => p.GetProperties())
                .Where(p => p.ClrType == typeof(string)
                    && p.GetColumnType() is null);

            foreach (var property in properties)
                property.SetIsUnicode(false);

            modelBuilder.ApplyConfigurationsFromAssembly(assembly: GetType().Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
