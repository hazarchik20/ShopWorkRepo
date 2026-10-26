using Domain.ValueObjects.Advart;
using Domain.Entities.Advert;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Infrastructure.Configurations.Advert
{

    public sealed class AdvertConfiguration : IEntityTypeConfiguration<Domain.Entities.Advert.Advert>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Advert.Advert> builder)
        {
            builder.ToTable("Adverts", table =>
            {
                table.HasCheckConstraint(
                    "CK_Adverts_Price",
                    "[Price] >= 0");
            });

            // Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            // User
            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Adverts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Title
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Description
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(5000);

            // Price
            builder.Property(x => x.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.IsPriceNegotiable)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.Town)
                .HasMaxLength(150)
                .IsRequired(false);

            // Dates
            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.Property(x => x.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.Property(x => x.DeletedAt)
                .IsRequired(false);


            // Enums
            builder.Property(x => x.StateOfUse)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.AdvertDealType)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.SellerType)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .HasDefaultValue(AdvertStatus.Active)
                .IsRequired();

            // Genres - JSON
            var genresComparer = new ValueComparer<List<AdvertGenres>>(
                (a, b) => a!.SequenceEqual(b!),
                v => v.Aggregate(
                    0,
                    (hash, genre) => HashCode.Combine(hash, genre)),
                v => v.ToList()
            );

            var genresProperty = builder.Property(x => x.Genres)
                .HasConversion(
                    v => JsonSerializer.Serialize(
                        v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<AdvertGenres>>(
                        v, (JsonSerializerOptions?)null)!)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            genresProperty.Metadata.SetValueComparer(genresComparer);

            // Images

            var imagesComparer = new ValueComparer<List<string>>(
                (a, b) => a!.SequenceEqual(b!),
                v => v.Aggregate(
                    0,
                    (hash, image) => HashCode.Combine(hash, image)),
                v => v.ToList()
            );

            var imagesProperty = builder.Property(x => x.ImagesLinks)
                .HasConversion(
                    v => JsonSerializer.Serialize(
                        v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(
                        v, (JsonSerializerOptions?)null)!)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            imagesProperty.Metadata.SetValueComparer(imagesComparer);


            // Indexes
            builder.HasIndex(x => x.UserId);

            builder.HasIndex(x => x.Status);

            builder.HasIndex(x => x.CreatedAt);

            builder.HasIndex(x => x.DeletedAt);

            builder.HasIndex(x => x.Price);

            builder.HasIndex(x => new
            {
                x.Status,
                x.CreatedAt
            });
        }
    }

}
