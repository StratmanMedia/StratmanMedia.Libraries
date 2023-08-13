using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StratmanMedia.Repositories.EFCore;

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
        Table.Attach(entity);
        Table.Add(entity);
        Context.Entry(entity).State = EntityState.Added;
        var result = Context.SaveChanges();
        if (result == 0) throw new InvalidOperationException("No new rows were created in the database.");
    }

    public virtual async Task CreateAsync(TEntity entity, CancellationToken ct = new())
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        Table.Attach(entity);
        await Table.AddAsync(entity, ct);
        Context.Entry(entity).State = EntityState.Added;
        var result = await Context.SaveChangesAsync(ct);
        if (result == 0) throw new InvalidOperationException("No new rows were created in the database.");
    }

    public void Update(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        Table.Attach(entity);
        Table.Update(entity);
        Context.SaveChanges();
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken ct = new())
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        Table.Attach(entity);
        Table.Update(entity);
        await Context.SaveChangesAsync(ct);
    }

    public virtual void Delete(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        Table.Attach(entity);
        Table.Remove(entity);
        Context.SaveChanges();
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken ct = new())
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        Table.Attach(entity);
        Table.Remove(entity);
        await Context.SaveChangesAsync(ct);
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));
        Table.AttachRange(entities);
        Table.RemoveRange(entities);
        Context.SaveChanges();
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct = new())
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));
        Table.AttachRange(entities);
        Table.RemoveRange(entities);
        await Context.SaveChangesAsync(ct);
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        return Table.ToArray();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = new())
    {
        return await Task.Run(GetAll, ct);
    }
}