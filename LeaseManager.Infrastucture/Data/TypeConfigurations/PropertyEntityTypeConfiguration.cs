using LeaseManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaseManager.Core.Infrastuctures.Data.TypeConfigurations
{
    /// <summary>
    /// Configuration du type Property pour Entity Framework Core.
    /// Définit les contraintes, les types de colonnes, et les relations avec les autres entités.
    /// </summary>
    public class PropertyEntityTypeConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            // Nom de la table
            builder.ToTable("Properties");

            #region Clé primaire
            builder.HasKey(p => p.Id);
            #endregion

            #region Propriétés
            builder.Property(p => p.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Address)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.City)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.PostalCode)
                   .HasMaxLength(10);

            builder.Property(p => p.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Rooms)
                   .IsRequired();

            builder.Property(p => p.Bathroom)
                   .IsRequired();

            builder.Property(p => p.Surface)
                   .HasColumnType("float");

            builder.Property(p => p.Description)
                   .HasMaxLength(1000);

            builder.Property(p => p.CreatedAt)
                   .HasColumnType("datetime2");

            // Enumération : Type de propriété
            builder.Property(p => p.Type)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(50);

            // Enumération : Statut de la propriété
            builder.Property(p => p.Status)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(50);
            #endregion

            #region Relations
            // Une propriété appartient à un propriétaire (User)
            builder.HasOne(p => p.Owner)
                   .WithMany(u => u.OwnedProperties)
                   .HasForeignKey(p => p.OwnerId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Une propriété peut avoir plusieurs images
            builder.HasMany(p => p.Images)
                   .WithOne(i => i.Property)
                   .HasForeignKey(i => i.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Une propriété peut être liée à plusieurs baux
            builder.HasMany(p => p.Leases)
                   .WithOne(l => l.Property)
                   .HasForeignKey(l => l.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
