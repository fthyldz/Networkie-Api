using Networkie.Entities;

namespace Networkie.Persistence.Abstractions.Repositories;

public interface IDistrictRepository : IRepository<District>
{
    Task<IEnumerable<District>> GetAllByCityIdAsync(Guid cityId, CancellationToken cancellationToken = default);
    Task<District?> GetByCityIdAndNameAsync(Guid cityId, string name, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<District>> GetByContainsNameAsync(string name, CancellationToken cancellationToken = default);
}