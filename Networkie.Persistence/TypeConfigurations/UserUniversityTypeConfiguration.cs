using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Networkie.Entities;

namespace Networkie.Persistence.TypeConfigurations;

public class UserUniversityTypeConfiguration : IEntityTypeConfiguration<UserUniversity>
{
    public void Configure(EntityTypeBuilder<UserUniversity> builder)
    {
        builder.HasKey(x => new { x.UserId, x.UniversityId, x.DepartmentId });

        builder.HasIndex(x => new { x.UserId, x.UniversityId, x.DepartmentId, x.EntryYear })
            .IsUnique();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.UniversityId)
            .IsRequired();

        builder.Property(x => x.DepartmentId)
            .IsRequired();

        builder.Property(x => x.EntryYear)
            .IsRequired(false);

        builder.HasOne<User>()
            .WithMany(x => x.UserUniversities)
            .HasForeignKey(x => x.UserId);
    }
}