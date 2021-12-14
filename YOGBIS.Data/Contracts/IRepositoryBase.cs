using System;
using System.Linq;
using System.Linq.Expressions;

namespace YOGBIS.Data.Contracts
{
    public interface IRepositoryBase<T> where T : class, new()
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = null);

        T Get(int id);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
