using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Networkie.Entities;
using Networkie.Persistence.EntityFrameworkCore.Converters;

namespace Networkie.Persistence.TypeConfigurations;

public class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PasswordHashed)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PasswordSalt)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.MiddleName)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(x => x.LastName)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(x => x.PhoneCountryCode)
            .IsRequired(false)
            .HasMaxLength(5);

        builder.Property(x => x.PhoneNumber)
            .IsRequired(false)
            .HasMaxLength(20);

        builder.Property(x => x.Gender)
            .IsRequired(false);

        builder.Property(x => x.BirthOfDate)
            .HasConversion<UtcDateTimeConverter>()
            .IsRequired(false);

        builder.Property(x => x.IsEmployed)
            .IsRequired(false);
        
        builder.Property(x => x.IsSeekingForJob)
            .IsRequired(false);

        builder.Property(x => x.IsHiring)
            .IsRequired(false);

        builder.Property(x => x.ProfessionId)
            .IsRequired(false);

        builder.Property(x => x.CountryId)
            .IsRequired(false);

        builder.Property(x => x.StateId)
            .IsRequired(false);

        builder.Property(x => x.CityId)
            .IsRequired(false);

        builder.Property(x => x.DistrictId)
            .IsRequired(false);

        builder.Property(x => x.IsEmailVerified)
            .IsRequired();
        
        builder.Property(x => x.IsProfileCompleted)
            .IsRequired();
    }
}