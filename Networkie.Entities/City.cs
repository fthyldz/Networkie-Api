using Networkie.Entities.Abstractions;

namespace Networkie.Entities;

public class City : BaseEntity
{
    public Guid CountryId { get; set; }
    public Guid? StateId { get; set; }
    public string Name { get; set; }
}