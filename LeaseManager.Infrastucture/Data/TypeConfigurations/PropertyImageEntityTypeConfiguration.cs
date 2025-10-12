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
            #region IEntityTypeConfiguration Implementation

            /// <summary>
            /// Configure les propriétés et les relations de l'entité PropertyImage.
            /// </summary>
            /// <param name="builder">Le constructeur d'entité pour PropertyImage.</param>
            public void Configure(EntityTypeBuilder<PropertyImage> builder)
            {
                #region Table & Key
                builder.ToTable("PropertyImages");

                builder.HasKey(pi => pi.Id);

                #endregion

                #region Properties

                builder.Property(pi => pi.ImageUrl)
                       .IsRequired()
                       .HasMaxLength(500);

                #endregion

                #region Relationships
                builder.HasOne(pi => pi.Property)
                       .WithMany(p => p.Images)
                       .HasForeignKey(pi => pi.PropertyId)
                       .OnDelete(DeleteBehavior.Cascade);

                #endregion
            }

            #endregion
        }
}
