using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StratmanMedia.Repositories.EFCore
{
    public class Repository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        protected TContext Context { get; }
        protected DbSet<TEntity> Table => Context.Set<TEntity>();

        public Repository(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual void Create(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Table.Add(entity);
            Context.SaveChanges();
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await Table.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Table.Attach(entity);
            Table.Remove(entity);
            Context.SaveChanges();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Table.Attach(entity);
            Table.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Table.ToArray();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.Run(GetAll);
        }
    }
}