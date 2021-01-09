using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyWebAPITemplate.Source.Core.Entities;

namespace MyWebAPITemplate.Source.Core.Interfaces.Database
{
    /// <summary>
    /// Base database methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEfRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Get selected type entity from database with given id
        /// </summary>
        /// <param name="id">Entity's id</param>
        /// <returns>Found entity or null</returns>
        Task<T> GetByIdAsync(Guid id);
        
        /// <summary>
        /// Get all selected type entities from database
        /// </summary>
        /// <returns>Found entities or empty</returns>
        Task<IReadOnlyList<T>> ListAllAsync();

        /// <summary>
        /// Add new selected type entity to database
        /// </summary>
        /// <param name="entity">Entity with values</param>
        /// <returns>Created entity with generated id</returns>
        Task<T> AddAsync(T entity);
        
        /// <summary>
        /// Update existing selected type entity from database
        /// </summary>
        /// <param name="entity">Existing entity with updated values</param>
        /// <returns></returns>
        Task UpdateAsync(T entity);
        
        /// <summary>
        /// Delete existing selected type entity from database
        /// </summary>
        /// <param name="entity">Existing entity</param>
        /// <returns></returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// TODO: Implement
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        //Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec); 

        /// <summary>
        /// TODO: Implement
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        //Task<int> CountAsync(ISpecification<T> spec);

        /// <summary>
        /// TODO: Implement
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        //Task<T> FirstAsync(ISpecification<T> spec);

        /// <summary>
        /// TODO: Implement
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        //Task<T> FirstOrDefaultAsync(ISpecification<T> spec);
    }
}
