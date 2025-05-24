using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Networkie.Entities;
using Networkie.Persistence.EntityFrameworkCore.Converters;

namespace Networkie.Persistence.TypeConfigurations;

public class UserEmailVerificationTypeConfiguration : IEntityTypeConfiguration<UserEmailVerification>
{
    public void Configure(EntityTypeBuilder<UserEmailVerification> builder)
    {
        builder.HasKey(x => new { x.UserId, x.VerificationCode});

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ExpiresAt)
            .HasConversion<UtcDateTimeConverter>()
            .IsRequired();
        
        builder.HasOne<User>()
            .WithOne(x => x.UserEmailVerification)
            .HasForeignKey<UserEmailVerification>(x => x.UserId);
    }
}