﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Extensions.Events.Common
{
    public static class SoftDeleteExtensions
    {
        public static IQueryable<T> IsDeleted<T>(this IQueryable<T> query) where T : ISupportSoftDelete
        {
            return query.Where(x => x.IsDeleted);
        }
        
        public static IQueryable<T> IsNotDeleted<T>(this IQueryable<T> query) where T : ISupportSoftDelete
        {
            return query.Where(x => !x.IsDeleted);
        }
        
        public static T SoftDelete<T>(this T entity) where T : ISupportSoftDelete
        {
            entity.IsDeleted = true;

            return entity;
        }
        
        public static TEntity SoftDelete<TEntity>(this DbSet<TEntity> dbSet, params object[] key) where TEntity : class, ISupportSoftDelete
        {
            var entity = dbSet.Find(key);
            return entity.SoftDelete();
        }
        
        public static TEntity SoftDelete<TEntity>(this DbContext context, params object[] key) where TEntity : class, ISupportSoftDelete
        {
            var entity = context.Find<TEntity>(key);
            return entity.SoftDelete();
        }
        
        public static async Task<TEntity> SoftDeleteAsync<TEntity>(this DbSet<TEntity> dbSet, object[] key, CancellationToken cancellationToken = default) where TEntity : class, ISupportSoftDelete
        {
            var entity = await dbSet.FindAsync(key, cancellationToken);
            return entity.SoftDelete();
        }
        
        public static async Task<TEntity> SoftDeleteAsync<TEntity>(this DbContext context, object[] key, CancellationToken cancellationToken = default) where TEntity : class, ISupportSoftDelete
        {
            var entity = await context.FindAsync<TEntity>(key, cancellationToken);
            return entity.SoftDelete();
        }
    }
}