﻿
using Microsoft.EntityFrameworkCore;
using Mosbi.Application.Abstracts.Repositories.Base;
using Mosbi.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace Mosbi.Infrastructure.Concretes.Repositories.Base;

public class Repository<TEntity> : IRepository<TEntity>
       where TEntity : class
{
    private readonly MosbiDbContext _context;

    public Repository(MosbiDbContext context)
    {
        _context = context;
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
    params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return filter is null
            ? await query.ToListAsync()
            : await query.Where(filter).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetPaginatedAsync(int page, int pageSize)
    {
        return await _context.Set<TEntity>()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return filter is null
            ? await query.FirstOrDefaultAsync()
            : await query.Where(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<int> GetTotalCountAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        return filter is null
            ? await _context.Set<TEntity>().CountAsync()
            : await _context.Set<TEntity>().Where(filter).CountAsync();
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _context.Set<TEntity>().AnyAsync(filter);
    }
}
