using Domain.Entities.Advert;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Advert
{
    public sealed class AdvertImageConfiguration
        : IEntityTypeConfiguration<AdvertImage>
    {
        public void Configure(EntityTypeBuilder<AdvertImage> builder)
        {
            builder.ToTable("AdvertImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ImageData)
                .HasColumnType("varbinary(max)")
                .IsRequired();

            builder.Property(x => x.ContentType)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(x => x.Advert)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.AdvertId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
