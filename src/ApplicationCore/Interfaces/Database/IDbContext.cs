//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Internal;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ApplicationCore.Interfaces.Database
//{
//    /// <summary>
//    /// Subset interface for Microsoft.EntityFrameworkCore.DbContext. If missing members are required, copy from there..
//    /// </summary>
//    public interface IDbContext : IDisposable, IInfrastructure<IServiceProvider>, IDbContextDependencies, IDbSetCache, IDbContextPoolable
//    {
//        DatabaseFacade Database { get; }

//        DbSet<Tentity> Set<Tentity>() where Tentity : class;

//        int SaveChanges();

//        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
//    }
//}