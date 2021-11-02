using Eccosys.Workflow.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Eccosys.Workflow.Infrastructure.EntityFramework
{
    public sealed class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> context) : base(context) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = this.ChangeTracker.Entries().Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {                
                if (entity.State == EntityState.Added)
                {
                    ((BaseModel)entity.Entity).CreatedAt = DateTime.UtcNow;
                }

                ((BaseModel)entity.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.Model.GetEntityTypes().ToList().ForEach(entity =>
            {
                entity.SetTableName(ToSnakeCase(entity.DisplayName()));
                entity.GetProperties().ToList().ForEach(prop => prop.SetColumnName(ToSnakeCase(prop.Name)));
                entity.GetKeys().ToList().ForEach(key => key.SetName(ToSnakeCase(key.GetName())));
                entity.GetForeignKeys().ToList().ForEach(key => key.SetConstraintName(ToSnakeCase(key.GetConstraintName())));
                entity.GetIndexes().ToList().ForEach(index => index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName())));

                entity.GetProperties().Where(p => p.ClrType == typeof(Guid)).ToList().ForEach(property =>
                {
                    if (property.GetDefaultValueSql() == null && property.GetColumnBaseName().ToLower() == "id")
                    {
                        property.SetDefaultValueSql("uuid_generate_v4()");
                    }
                });
            });

            //modelBuilder.ApplyConfiguration(new CraConfiguration());
        }        

        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var noLeadingUndescore = Regex.Replace(input, @"^_", "");
            return Regex.Replace(noLeadingUndescore, @"(?:(?<l>[a-z0-9])(?<r>[A-Z])|(?<l>[A-Z])(?<r>[A-Z][a-z0-9]))", "${l}_${r}").ToLower();
        }
    }
}
