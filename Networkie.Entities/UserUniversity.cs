using Networkie.Entities.Abstractions;

namespace Networkie.Entities;

public class UserUniversity : IBaseEntity
{
    public Guid UserId { get; set; }
    public Guid UniversityId { get; set; }
    public Guid DepartmentId { get; set; }
    public short UniversityEntryYear { get; set; }
}