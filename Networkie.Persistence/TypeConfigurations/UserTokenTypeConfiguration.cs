using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Networkie.Entities;
using Networkie.Persistence.EntityFrameworkCore.Converters;

namespace Networkie.Persistence.TypeConfigurations;

public class UserTokenTypeConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.HasKey(x => new { x.UserId, x.Token, x.RefreshToken });

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.TokenCreatedAt)
            .HasConversion<UtcDateTimeConverter>()
            .IsRequired();

        builder.Property(x => x.TokenExpiresAt)
            .HasConversion<UtcDateTimeConverter>()
            .IsRequired();
        
        builder.Property(x => x.RefreshToken)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(x => x.RefreshTokenCreatedAt)
            .HasConversion<UtcDateTimeConverter>()
            .IsRequired();

        builder.Property(x => x.RefreshTokenExpiresAt)
            .HasConversion<UtcDateTimeConverter>()
            .IsRequired();
    }
}