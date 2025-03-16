using Networkie.Entities.Abstractions;

namespace Networkie.Entities;

public class District : BaseEntity
{
    public Guid CityId { get; set; }
    public string Name { get; set; }
}