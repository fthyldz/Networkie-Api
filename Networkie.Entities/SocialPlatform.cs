using Networkie.Entities.Abstractions;

namespace Networkie.Entities;

public class SocialPlatform : BaseEntity
{
    public string Name { get; set; }
    public bool IsRequired { get; set; }
}