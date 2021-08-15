using System.Collections.Generic;
using System.Threading.Tasks;

namespace StratmanMedia.Repositories.EFCore
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        Task CreateAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}