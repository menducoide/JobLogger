using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using JobLoggerCORE.IRepositories;
using JobLoggerCORE.Data;

namespace JobLoggerCORE.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {


        protected DbSet<T> dbSet { get; set; }
        protected JobLoggerContext context { get; set; }

        public BaseRepository()
        {
            this.context = new JobLoggerContext();
            dbSet = this.context.Set<T>();
        }

        public virtual void Delete(object id)
        {
            T entity = dbSet.Find(id);
            this.Delete(entity);
        }

        public virtual void Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            if (where != null)
            {
                IQueryable<T> query = dbSet;
                query = query.Where(where);
                dbSet.RemoveRange(query.ToList());
                context.SaveChanges();
            }
        }

        public virtual T Find(Expression<Func<T, bool>> where)
        {
            IQueryable<T> query = dbSet;
            if (where != null)
            {
                query = query.Where(where);
                return query.FirstOrDefault();
            }
            return null;
        }

        public virtual T Find(object id)
        {
            return dbSet.Find(id);
        }

        public virtual List<T> FindAll(Expression<Func<T, bool>> where = null)
        {
            IQueryable<T> query = dbSet;
            if (where != null)
            {
                query = query.Where(where);
                
            }
            return query.ToList();

        }

        public virtual T Insert(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public virtual List<T> Insert(List<T> entities)
        {
            dbSet.AddRange(entities);
            context.SaveChanges();
            return entities;
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
