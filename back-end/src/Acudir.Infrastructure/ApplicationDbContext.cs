using System;
using Acudir.Domain;
using Acudir.Domain.Interfaces;
using Acudir.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

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

