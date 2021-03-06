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
        #region - Variable

        private readonly ApplicationContext _context;

        #endregion

        #region - Ctor

        public EntityRepository()
        {
            _context = new ApplicationContext();
        }

        #endregion

        #region - Add

        public void Add(T entity)
        {
            entity.CreationDate = DateTime.Now;
            entity.IsItDeleted = false;
            entity.Status = true;

            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();
        }

        #endregion

        #region - Any

        public bool Any(Expression<Func<T, bool>> query = null, params Expression<Func<T, object>>[] includeExpressions)
        {
            return query == null ? _context.Set<T>().Any(x => x.IsItDeleted == false) : _context.Set<T>().Where(x => x.IsItDeleted == false).Any(query);
        }

        #endregion

        #region - Delete

        public void Delete(T entity)
        {
            entity.IsItDeleted = true;
            entity.Status = false;
            entity.DateOfUpdate = DateTime.Now;
            Update(entity);
            _context.SaveChanges();
        }

        #endregion

        #region - Delete

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

        #endregion

        #region - Get

        public virtual T Get(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includeExpressions)
        {
            var newQuery = _context.Set<T>().Where(a => a.IsItDeleted == false);
            if (includeExpressions.Any())
            {
                newQuery = includeExpressions.Aggregate(newQuery, (current, expressions) => current.Include(expressions));
            }
            return newQuery.SingleOrDefault(query);
        }

        #endregion

        #region - Get Count

        public int GetCount(Expression<Func<T, bool>> query = null, params Expression<Func<T, object>>[] includeExpressions)
        {
            var newQuery = _context.Set<T>().Where(a => a.IsItDeleted == false);
            if (includeExpressions.Any())
            {
                newQuery = includeExpressions.Aggregate(newQuery, (current, expressions) => current.Include(expressions));
            }
            return newQuery.Count();
        }

        #endregion

        #region - Get List

        public IQueryable<T> GetList(Expression<Func<T, bool>> query = null, params Expression<Func<T, object>>[] includeExpressions)
        {
            var newQuery = _context.Set<T>().Where(a => a.IsItDeleted == false);
            if (includeExpressions.Any())
            {
                newQuery = includeExpressions.Aggregate(newQuery, (current, expressions) => current.Include(expressions));
            }
            return newQuery;
        }

        #endregion

        #region - Update

        public void Update(T entity)
        {
            var updatedEntity = _context.Attach(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion
    }
}
