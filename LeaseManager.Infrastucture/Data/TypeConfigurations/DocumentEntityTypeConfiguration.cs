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
    public class DocumentEntityTypeConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");

            // Primary Key
            builder.HasKey(d => d.Id);

            // Properties
            builder.Property(d => d.FileName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(d => d.FilePath)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(d => d.UploadedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            builder.HasOne(d => d.Lease)
                   .WithMany(l => l.Documents)
                   .HasForeignKey(d => d.LeaseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
