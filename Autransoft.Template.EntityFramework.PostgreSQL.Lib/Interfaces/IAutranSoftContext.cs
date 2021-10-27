using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Autransoft.Template.EntityFramework.PostgreSQL.Lib.Interfaces
{
    public interface IAutranSoftContext
    {
        DbContextId ContextId { get; }
        IModel Model { get; }
        ChangeTracker ChangeTracker { get; }
        DatabaseFacade Database { get; }
        event EventHandler<SavingChangesEventArgs> SavingChanges;
        event EventHandler<SavedChangesEventArgs> SavedChanges;
        event EventHandler<SaveChangesFailedEventArgs> SaveChangesFailed;
        EntityEntry Add([NotNullAttribute] object entity);
        EntityEntry<TEntity> Add<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        ValueTask<EntityEntry> AddAsync([NotNullAttribute] object entity, CancellationToken cancellationToken = default);
        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>([NotNullAttribute] TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        void AddRange([NotNullAttribute] IEnumerable<object> entities);
        void AddRange([NotNullAttribute] params object[] entities);
        Task AddRangeAsync([NotNullAttribute] IEnumerable<object> entities, CancellationToken cancellationToken = default);
        Task AddRangeAsync([NotNullAttribute] params object[] entities);
        EntityEntry<TEntity> Attach<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        EntityEntry Attach([NotNullAttribute] object entity);
        void AttachRange([NotNullAttribute] params object[] entities);
        void AttachRange([NotNullAttribute] IEnumerable<object> entities);
        void Dispose();
        ValueTask DisposeAsync();
        EntityEntry Entry([NotNullAttribute] object entity);
        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        //TEntity Find<TEntity>([CanBeNullAttribute] params object[] keyValues) where TEntity : class;
        //object Find([NotNullAttribute] Type entityType, [CanBeNullAttribute] params object[] keyValues);
        //ValueTask<TEntity> FindAsync<TEntity>([CanBeNullAttribute] object[] keyValues, CancellationToken cancellationToken) where TEntity : class;
        //ValueTask<object> FindAsync([NotNullAttribute] Type entityType, [CanBeNullAttribute] object[] keyValues, CancellationToken cancellationToken);
        //ValueTask<TEntity> FindAsync<TEntity>([CanBeNullAttribute] params object[] keyValues) where TEntity : class;
        //ValueTask<object> FindAsync([NotNullAttribute] Type entityType, [CanBeNullAttribute] params object[] keyValues);
        IQueryable<TResult> FromExpression<TResult>([NotNullAttribute] Expression<Func<IQueryable<TResult>>> expression);
        EntityEntry Remove([NotNullAttribute] object entity);
        EntityEntry<TEntity> Remove<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        void RemoveRange([NotNullAttribute] params object[] entities);
        void RemoveRange([NotNullAttribute] IEnumerable<object> entities);
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>([NotNullAttribute] string name) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();
        EntityEntry Update([NotNullAttribute] object entity);
        EntityEntry<TEntity> Update<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        void UpdateRange([NotNullAttribute] params object[] entities);
        void UpdateRange([NotNullAttribute] IEnumerable<object> entities);
    }
}