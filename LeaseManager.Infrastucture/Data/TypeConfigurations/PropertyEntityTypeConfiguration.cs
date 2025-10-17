using LeaseManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaseManager.Core.Infrastuctures.Data.TypeConfigurations
{
    /// <summary>
    /// Configuration du type Property pour Entity Framework Core.
    /// Définit les contraintes, les types de colonnes, et les relations avec les autres entités.
    /// </summary>
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Properties");

            // Primary Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Title)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.Address)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(p => p.City)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Province)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.PostalCode)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(p => p.RentPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Bedrooms)
                   .IsRequired();

            builder.Property(p => p.Bathrooms)
                   .IsRequired();

            builder.Property(p => p.IsAvailable)
                   .IsRequired()
                   .HasDefaultValue(true);

            // Relationships
            builder.HasOne(p => p.Owner)
                   .WithMany(o => o.Properties)
                   .HasForeignKey(p => p.OwnerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Images)
                   .WithOne(i => i.Property)
                   .HasForeignKey(i => i.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Leases)
                   .WithOne(l => l.Property)
                   .HasForeignKey(l => l.PropertyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}