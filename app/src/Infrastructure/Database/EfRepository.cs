using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Core.Interfaces.Database;

namespace MyWebAPITemplate.Source.Infrastructure.Database;

// This class is derived from https://github.com/dotnet-architecture/eShopOnWeb/blob/master/src/Infrastructure/Data/EfRepository.cs
///<inheritdoc/>
public class EfRepository<T> : IEfRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _dbContext;

    public EfRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    //public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    //{
    //    return await ApplySpecification(spec).ToListAsync();
    //}

    //public async Task<int> CountAsync(ISpecification<T> spec)
    //{
    //    return await ApplySpecification(spec).CountAsync();
    //}

    //public async Task<T> FirstAsync(ISpecification<T> spec)
    //{
    //    return await ApplySpecification(spec).FirstAsync();
    //}

    //public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec)
    //{
    //    return await ApplySpecification(spec).FirstOrDefaultAsync();
    //}

    //private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    //{
    //    return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
    //}
}