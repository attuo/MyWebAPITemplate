using Microsoft.EntityFrameworkCore;
using MyWebAPITemplate.Source.Core.Exceptions;
using MyWebAPITemplate.Source.Core.Interfaces.Database;

namespace MyWebAPITemplate.Source.Infrastructure.Database;

// This class is derived from https://github.com/dotnet-architecture/eShopOnWeb/blob/master/src/Infrastructure/Data/EfRepository.cs

/// <inheritdoc/>
public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
    /// </summary>
    /// <param name="dbContext">See <see cref="ApplicationDbContext"/>.</param>
    protected RepositoryBase(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id)
            ?? throw new EntityNotFoundException(id.ToString());
    }

    /// <inheritdoc />
    public async Task<T> AddAsync(T entity)
    {
        _ = await _dbContext.Set<T>().AddAsync(entity);
        _ = await _dbContext.SaveChangesAsync();

        return entity;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(T entity)
    {
        _ = _dbContext.Entry(entity).State = EntityState.Modified;
        _ = await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(T entity)
    {
        _ = _dbContext.Set<T>().Remove(entity);
        _ = await _dbContext.SaveChangesAsync();
    }
}