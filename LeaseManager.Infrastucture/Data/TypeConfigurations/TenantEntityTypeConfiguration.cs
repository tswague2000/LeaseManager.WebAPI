using LeaseManager.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManager.Infrastucture.Data.TypeConfigurations
{
    public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");

            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.FullName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(t => t.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(t => t.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            // Relationships
            builder.HasMany(t => t.Leases)
                   .WithOne(l => l.Tenant)
                   .HasForeignKey(l => l.TenantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}