using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Networkie.Persistence.Abstractions;
using Networkie.Persistence.EntityFrameworkCore.Contexts;

namespace Networkie.Persistence.EntityFrameworkCore;

public class Repository<TEntity>(IEfCoreDbContext context) : IRepository<TEntity> where TEntity : class
{
    protected IEfCoreDbContext Context => context;
    protected readonly DbSet<TEntity> Table = context.Set<TEntity>();
    protected IQueryable<TEntity> TableAsNoTracking => Table.AsNoTracking();

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await Table.FindAsync([id], cancellationToken);

    public async Task<TEntity?> FindBySearchAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) =>
        await Table.FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await Table.ToListAsync(cancellationToken);

    public async Task<IEnumerable<TEntity>> FindAllBySearchAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) =>
        await Table.Where(predicate).ToListAsync(cancellationToken);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        await Table.AddAsync(entity, cancellationToken);

    public void Update(TEntity entity) => Table.Update(entity);
    public void Delete(TEntity entity) => Table.Remove(entity);
}