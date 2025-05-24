using Networkie.Entities.Abstractions;

namespace Networkie.Entities;

public class UserUniversity : IBaseEntity
{
    public Guid UserId { get; set; }
    public Guid UniversityId { get; set; }
    public virtual University University { get; set; }
    public Guid DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    public short? EntryYear { get; set; }
}