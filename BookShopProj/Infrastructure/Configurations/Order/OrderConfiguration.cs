using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configurations.Order
{
    public sealed class OrderConfiguration
        : IEntityTypeConfiguration<Domain.Entities.Order.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order.Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Number)
                .UseIdentityColumn(1, 1);

            builder.HasIndex(x => x.Number)
                .IsUnique();
            builder.Property(x => x.Number)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(1, 1);

            builder.Ignore(x => x.OrderNumber);

            builder.Property(x => x.BookTitle)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ImageLink)
                .HasMaxLength(1000);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.HasIndex(x => x.BuyerId);
            builder.HasIndex(x => x.SellerId);
            builder.HasIndex(x => x.AdvertId);

            builder.HasOne<Domain.Entities.User.User>()
                .WithMany()
                .HasForeignKey(x => x.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Domain.Entities.User.User>()
                .WithMany()
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Domain.Entities.Advert.Advert>()
                .WithMany()
                .HasForeignKey(x => x.AdvertId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
