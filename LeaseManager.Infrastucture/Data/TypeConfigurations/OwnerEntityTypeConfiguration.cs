using LeaseManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaseManager.Infrastucture.Data.TypeConfigurations
{
    public class OwnerEntityTypeConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("Owners");

            // Primary Key
            builder.HasKey(o => o.Id);

            // Properties
            builder.Property(o => o.FullName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(o => o.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(o => o.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            // Relationships
            builder.HasMany(o => o.Properties)
                   .WithOne(p => p.Owner)
                   .HasForeignKey(p => p.OwnerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
