using LeaseManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaseManager.Infrastructure.Persistence.Configurations
{
        /// <summary>
        /// Configuration de l'entité PropertyImage pour Entity Framework Core.
        /// Définit la table, la clé primaire, les propriétés et les relations.
        /// </summary>
        public class PropertyImageEntityTypeConfiguration : IEntityTypeConfiguration<PropertyImage>
        {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.ToTable("PropertyImages");

            // Primary Key
            builder.HasKey(pi => pi.Id);

            // Properties
            builder.Property(pi => pi.ImageUrl)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(pi => pi.Description)
                   .HasMaxLength(250);

            // Relationships
            builder.HasOne(pi => pi.Property)
                   .WithMany(p => p.Images)
                   .HasForeignKey(pi => pi.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
