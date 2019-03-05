using System;
using System.Collections.Generic;
using System.Linq.Expressions;
 

namespace JobLoggerCORE.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        T Insert(T entity);
        List<T> Insert(List<T> entities);
        void Update(T entity);
        List<T> FindAll(Expression<Func<T, bool>> where = null);
        T Find(Expression<Func<T, bool>> where);
        T Find(object id);
        void Delete(object id);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);

    }
}
