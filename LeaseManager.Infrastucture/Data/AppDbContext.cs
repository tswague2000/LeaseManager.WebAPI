using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Infrastuctures.Data.TypeConfigurations;
using LeaseManager.Infrastructure.Persistence.Configurations;
using LeaseManager.Infrastucture.Data.TypeConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace LeaseManager.Core.Infrastuctures.Data
{
    /// <summary>
    /// Contexte de base de données principal pour l'application LeaseManager.
    /// Gère les entités, la configuration du modèle et l'accès aux données via Entity Framework Core.
    /// </summary>
    public class AppDbContext : DbContext
    {
        #region Constructors
        /// <summary>
        /// Initialise une nouvelle instance de <see cref="AppDbContext"/> avec les options spécifiées.
        /// </summary>
        /// <param name="options">Options de configuration du contexte.</param>
        public AppDbContext([NotNull] DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="AppDbContext"/> sans options.
        /// </summary>
        public AppDbContext() : base() { }
        #endregion

        #region Internal methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OwnerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TenantEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyImageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LeaseConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MaintenanceRequestEntityTypeConfiguration());
        }
        #endregion

        #region DbSets
        public DbSet<Property> Properties { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
        public DbSet<Lease> Leases { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        #endregion
    }
}