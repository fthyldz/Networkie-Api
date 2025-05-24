using Networkie.Entities.Abstractions;

namespace Networkie.Entities;

public class UserRole : IBaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; }
}