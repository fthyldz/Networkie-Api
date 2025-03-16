namespace Networkie.Entities.Abstractions;

public abstract class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }
}