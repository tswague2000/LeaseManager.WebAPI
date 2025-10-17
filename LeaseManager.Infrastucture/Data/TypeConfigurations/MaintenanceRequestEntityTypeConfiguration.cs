using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManager.Infrastucture.Data.TypeConfigurations
{
  
    public class MaintenanceRequestEntityTypeConfiguration : IEntityTypeConfiguration<MaintenanceRequest>
    {
        public void Configure(EntityTypeBuilder<MaintenanceRequest> builder)
        {
            builder.ToTable("MaintenanceRequests");

            // Primary Key
            builder.HasKey(m => m.Id);

            // Properties
            builder.Property(m => m.Description)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(m => m.RequestDate)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(m => m.Status)
                   .IsRequired()
                   .HasConversion<string>() // Store enum as readable string
                   .HasMaxLength(50)
                   .HasDefaultValue(MaintenanceStatus.Pending);

            // Relationships

            // Each maintenance request is linked to one property
            builder.HasOne(m => m.Property)
                   .WithMany()
                   .HasForeignKey(m => m.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Each maintenance request is submitted by one tenant
            builder.HasOne(m => m.Tenant)
                   .WithMany()
                   .HasForeignKey(m => m.TenantId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
