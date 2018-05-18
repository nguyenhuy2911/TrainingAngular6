using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TraningApp.WebAPI.Data.Infrastructure
{
    public class RepositoryBase<T> where T: class
    {
        protected ApplicationDbContext dataContext;
        protected readonly DbSet<T> dbset;
        protected RepositoryBase(ApplicationDbContext DataContext)
        {
            dataContext = DataContext;
            dbset = DataContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }
        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);

        }
        public virtual void Delete(long id)
        {
            var entity = GetById(id);
            dbset.Remove(entity);

        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }
        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }
        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            var results = dbset.ToList();
            return results;
        }
        public virtual IPaginatedList<T> GetMany(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int page = 1, int pageSize = 10, bool descending = true)
        {
            var query = dbset.Where(where);
            if (descending)
                query.OrderByDescending(order);
            var taskResult = PaginatedList<T>.CreateAsync(query.Where(where),page, pageSize);
            taskResult.Wait();
            return taskResult.Result;
        }
    }
}
