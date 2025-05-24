using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Networkie.Entities;

namespace Networkie.Persistence.TypeConfigurations;

public class CityTypeConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CountryId)
            .IsRequired();

        builder.Property(x => x.StateId);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}