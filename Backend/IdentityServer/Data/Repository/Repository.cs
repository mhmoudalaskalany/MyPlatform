﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Abstraction.Repository;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        protected DbSet<T> DbSet;
        public Repository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }
        
        public async Task<T> GetAsync(params object[] keys)
        {
            return await DbSet.FindAsync(keys);
        }
      
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (orderby != null)
            {
                query = orderby(query);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync();

        }
       
        public async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, IEnumerable<SortModel> orderByCriteria , Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (orderby != null)
            {
                query = orderby(query);
            }
            if (orderByCriteria != null)
            {
                query = query.OrderBy(orderByCriteria);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.LastOrDefaultAsync();

        }
 
        public async Task<IEnumerable<T>> GetAllAsync(IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (include != null)
            {
                query = include(query);
            }
            if (orderByCriteria != null)
            {
                query = query.OrderBy(orderByCriteria);
            }
            return await query.ToListAsync();
        }
        

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate = null, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderByCriteria != null)
            {
                query = query.OrderBy(orderByCriteria);
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }

        public async Task<(int, IEnumerable<T>)> FindPagedAsync(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            int count = query.Count();
            if (orderByCriteria != null)
            {
                query = query.OrderBy(orderByCriteria);
            }
            query = query.Skip(skip).Take(take);
            if (include != null)
            {
                query = include(query);
            }
            return (count, await query.ToListAsync());
        }

        public async Task<(int, IEnumerable<T>)> FindPagedWithOrderAsync(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                query = include(query);
            }
            int count = query.Count();
            if (orderByCriteria != null && orderByCriteria.Count() > 0)
            {
                var field = orderByCriteria.FirstOrDefault()?.PairAsSqlExpression;
                query = query.OrderBy(field);
            }
            query = query.Skip(skip).Take(take);

            return (count, await query.ToListAsync());
        }

        public async Task<IEnumerable<TType>> FindSelectAsync<TType>(Expression<Func<T, TType>> select, Expression<Func<T, bool>> predicate = null, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true) where TType : class
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderByCriteria != null)
            {
                query = query.OrderBy(orderByCriteria);
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.Select(select).ToListAsync();
        }
   
        public async Task<bool> Any(Expression<Func<T, bool>> predicate = null) =>
            predicate == null ? await DbSet.AnyAsync() : await DbSet.AnyAsync(predicate);
     
        public async Task<int> Count(Expression<Func<T, bool>> predicate = null) =>
            predicate == null ? await DbSet.CountAsync() : await DbSet.CountAsync(predicate);
      

        public T Add(T newEntity)
        {
            return DbSet.Add(newEntity).Entity;
        }
    
        public void AddRange(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
        }
      
        public void Update(T originalEntity, T newEntity)
        {
            Context.Entry(originalEntity).CurrentValues.SetValues(newEntity);
        }
      
        public async Task UpdateAsync(object id, T newEntity)
        {
            var originalEntity = await DbSet.FindAsync(id);
            Context.Entry(originalEntity).CurrentValues.SetValues(newEntity);
        }
       
        public void UpdateRange(IEnumerable<T> newEntities)
        {
            Context.UpdateRange(newEntities);
        }
        
        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
      
        public async void Remove(Expression<Func<T, bool>> predicate)
        {
            var objects = await DbSet.FindAsync(predicate);
            DbSet.RemoveRange(objects);
        }
     
        public void RemoveLogical(T entity)
        {
            var type = entity.GetType();
            var property = type.GetProperty("IsDeleted");
            if (property != null) property.SetValue(entity, true);
            var id = type.GetProperty("Id")?.GetValue(entity);
            var original = DbSet.Find(id);
            Update(original, entity);
        }
     
        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
     
        public long GetNextSequenceValue(string sequenceName)
        {
            var value = Context.GetNextSequenceValue(sequenceName);
            return value;
        }
    }
}
