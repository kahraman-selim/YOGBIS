using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using YOGBIS.Data.Contracts;
using YOGBIS.Data.DataContext;

namespace YOGBIS.Data.Implementaion
{
    public class Repository<T> : IRepositoryBase<T> where T : class, new()
    {
        #region Context
        private readonly YOGBISContext _ctx;
        internal DbSet<T> dbSet;
        #endregion

        #region Dönüştürücü
        public Repository(YOGBISContext ctx)
        {
            _ctx = ctx;
            this.dbSet = _ctx.Set<T>();
        }
        #endregion

        #region Add
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        #endregion

        #region Get
        public T Get(Guid id)
        {
            return dbSet.Find(id);
        }
        #endregion

        #region GetAll
        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            if (orderby != null)
            {
                return orderby(query);
            }

            return query;
        }
        #endregion

        #region GetFirstOrDefault
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            return query.FirstOrDefault();
        }
        #endregion

        #region Remove
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
        #endregion

        #region Update
        public void Update(T entity)
        {
            dbSet.Update(entity);
        } 
        #endregion
    }
}
