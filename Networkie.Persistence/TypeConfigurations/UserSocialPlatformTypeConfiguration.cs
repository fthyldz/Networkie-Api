using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Networkie.Entities;

namespace Networkie.Persistence.TypeConfigurations;

public class UserSocialPlatformTypeConfiguration : IEntityTypeConfiguration<UserSocialPlatform>
{
    public void Configure(EntityTypeBuilder<UserSocialPlatform> builder)
    {
        builder.HasKey(x => new { x.UserId, x.SocialPlatformId });

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.SocialPlatformId)
            .IsRequired();

        builder.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.HasOne<User>()
            .WithMany(x => x.UserSocialPlatforms)
            .HasForeignKey(x => x.UserId);
    }
}