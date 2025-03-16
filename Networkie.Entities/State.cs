using Networkie.Entities.Abstractions;

namespace Networkie.Entities;

public class State : BaseEntity
{
    public Guid CountryId { get; set; }
    public string Name { get; set; }
}