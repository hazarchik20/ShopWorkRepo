using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public sealed class UserSecurityConfiguration : IEntityTypeConfiguration<UserSecurity>
    {
        public void Configure(EntityTypeBuilder<UserSecurity> builder)
        {
            builder.ToTable("UserSecurities");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.RegistrationIp)
                .IsRequired()
                .HasMaxLength(45);

            builder.Property(x => x.RegistractionUserAgent)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.LastLogin);

            builder.HasIndex(x => x.UserId)
                .IsUnique();

            builder.HasIndex(x => x.RegistrationIp);
        }
    }
}
