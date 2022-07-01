using System;
using Acudir.Domain;
using Acudir.Domain.Interfaces;
using Acudir.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Acudir.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        #region Properties

        public DbSet<Persona> Personas { get; set; } = null!;

        #endregion

        #region Constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            setOnDeleteBehavior(modelBuilder, DeleteBehavior.Restrict);

            addSoftDeleteQueryFilter(modelBuilder);

            populatePersonas(modelBuilder);
        }

        #region SoftDelete Implementation

        private static void addSoftDeleteQueryFilter(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes().Where(e => e.BaseType == null && typeof(ISoftDelete).IsAssignableFrom(e.ClrType) && e.ClrType != null))
            {
                entityType.AddSoftDeleteQueryFilter();
            }
        }

        public override int SaveChanges()
        {
            entryStateHandler();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            entryStateHandler();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void entryStateHandler()
        {
            foreach (EntityEntry entry in ChangeTracker.Entries())
            {
                bool hasSoftDelete = entry.Entity is ISoftDelete;

                switch (entry.State)
                {
                    case EntityState.Added:
                        addSoftDeleteEntity(entry, hasSoftDelete);
                        break;

                    case EntityState.Modified:
                        modifySoftDeleteEntity(entry, hasSoftDelete);
                        break;

                    case EntityState.Deleted:
                        deleteSoftDeleteEntity(entry, hasSoftDelete);
                        break;
                }
            }
        }

        private static void deleteSoftDeleteEntity(EntityEntry entry, bool hasSoftDelete)
        {
            if (hasSoftDelete)
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["Activo"] = false;
            }
        }

        private static void modifySoftDeleteEntity(EntityEntry entry, bool hasSoftDelete)
        {
            if (hasSoftDelete)
                entry.CurrentValues["Activo"] = true;
        }

        private static void addSoftDeleteEntity(EntityEntry entry, bool hasSoftDelete)
        {
            if (hasSoftDelete)
                entry.CurrentValues["Activo"] = true;
        }

        private static void setOnDeleteBehavior(ModelBuilder builder, DeleteBehavior deleteBehavior)
        {
            var cascadeFKs = builder.Model.GetEntityTypes()
                            .SelectMany(t => t.GetForeignKeys())
                            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = deleteBehavior;
            }
        }

        #endregion

        #region Data Seeding

        private static void populatePersonas(ModelBuilder modelBuilder)
        {
            IEnumerable<Persona> personas = Enumerable.Range(1, 1000).Select(i => new Persona()
            {
                Id = i,
                Nombre = $"Persona {i}",
                Apellido = $"Apellido {i}",
                Provincia = $"Buenos Aires",
                DNI = Random.Shared.Next(1000000 + i, 4000000),
                Telefono = Random.Shared.Next(40000000 + i, 50000000),
                Mail = $"persona{i}@gmail.com",
                Activo = true
            });

            modelBuilder
                .Entity<Persona>()
                .HasData(personas);
        }

        #endregion
    }
}

