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
        private readonly YOGBISContext _ctx;
        internal DbSet<T> dbSet;
        public Repository(YOGBISContext ctx)
        {
            _ctx = ctx;
            this.dbSet = _ctx.Set<T>();
        }
        /// <summary>
        /// Gelen tipte veri kaydeden method
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter!=null)
            {
                query = query.Where(filter);
            }

            if (includeProperties!=null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            if (orderby!=null)
            {
                return orderby(query);
            }

            return query;
        }

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

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
