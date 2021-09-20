#region - Using

using GeoSys.DAL.Context;
using GeoSys.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace GeoSys.DAL.Repository
{
    public class EntityRepository<T>
        where T : BaseModel
    {
        private readonly ApplicationContext _context;

        public EntityRepository()
        {
            _context = new ApplicationContext();
        }

        public void Add(T entity)
        {
            entity.CreationDate = DateTime.Now;
            entity.IsItDeleted = false;
            entity.Status = true;

            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();
        }

        public bool Any(Expression<Func<T, bool>> query = null, params Expression<Func<T, object>>[] includeExpressions)
        {
            return query == null ? _context.Set<T>().Any(x => x.IsItDeleted == false) : _context.Set<T>().Where(x => x.IsItDeleted == false).Any(query);
        }

        public void Delete(T entity)
        {
            entity.IsItDeleted = true;
            entity.Status = false;
            entity.DateOfUpdate = DateTime.Now;
            Update(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> query)
        {
            if (query == null) throw new ArgumentNullException(typeof(T).Name + " sorgu boş olamaz.");

            ICollection<T> list = GetList(query).Where(query).ToList();
            foreach (var item in list)
            {
                item.IsItDeleted = true;
                item.Status = false;
                item.DateOfUpdate = DateTime.Now;
                Update(item);
            }
        }

        public virtual T Get(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includeExpressions)
        {
            var newQuery = _context.Set<T>().Where(a => a.IsItDeleted == false);
            if (includeExpressions.Any())
            {
                newQuery = includeExpressions.Aggregate(newQuery, (current, expressions) => current.Include(expressions));
            }
            return newQuery.SingleOrDefault(query);
        }

        public int GetCount(Expression<Func<T, bool>> query = null, params Expression<Func<T, object>>[] includeExpressions)
        {
            var newQuery = _context.Set<T>().Where(a => a.IsItDeleted == false);
            if (includeExpressions.Any())
            {
                newQuery = includeExpressions.Aggregate(newQuery, (current, expressions) => current.Include(expressions));
            }
            return newQuery.Count();
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> query = null, params Expression<Func<T, object>>[] includeExpressions)
        {
            var newQuery = _context.Set<T>().Where(a => a.IsItDeleted == false);
            if (includeExpressions.Any())
            {
                newQuery = includeExpressions.Aggregate(newQuery, (current, expressions) => current.Include(expressions));
            }
            return newQuery;
        }

        public void Update(T entity)
        {
            var updatedEntity = _context.Attach(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
