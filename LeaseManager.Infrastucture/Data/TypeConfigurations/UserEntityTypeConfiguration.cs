using LeaseManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaseManager.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Configuration EF Core pour l'entité <see cref="User"/>.
    /// Définit la structure de la table Users, les propriétés et les relations.
    /// </summary>
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Configuration de la table
            builder.ToTable("Users");
            #endregion

            #region Clé primaire
            builder.HasKey(u => u.Id);
            #endregion

            #region Configuration des propriétés
            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(20);

            builder.Property(u => u.Role)
                   .IsRequired();
            #endregion

            #region Configuration des relations
            builder.HasMany(u => u.OwnedProperties)
                   .WithOne(p => p.Owner)
                   .HasForeignKey(p => p.OwnerId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(u => u.Leases)
                   .WithOne(l => l.Tenant)
                   .HasForeignKey(l => l.TenantId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
