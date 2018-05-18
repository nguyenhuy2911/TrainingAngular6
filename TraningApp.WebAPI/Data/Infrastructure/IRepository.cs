using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TraningApp.WebAPI.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(long id);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long id);
        T GetById(string id);
        IEnumerable<T> GetAll();
        IPaginatedList<T> GetMany(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int page = 1, int pageSize = 10, bool descending = true);
    }
}
