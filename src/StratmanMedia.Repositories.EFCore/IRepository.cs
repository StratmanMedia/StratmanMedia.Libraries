using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StratmanMedia.Repositories.EFCore;

public interface IRepository<TEntity> where TEntity : class
{
    void Create(TEntity entity);
    Task CreateAsync(TEntity entity, CancellationToken ct = new());
    void Update(TEntity entity);
    Task UpdateAsync(TEntity entity, CancellationToken ct = new());
    void Delete(TEntity entity);
    Task DeleteAsync(TEntity entity, CancellationToken ct = new());
    void DeleteRange(IEnumerable<TEntity> entities);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct = new());
    IEnumerable<TEntity> GetAll();
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = new());
}