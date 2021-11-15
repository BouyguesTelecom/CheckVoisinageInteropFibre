using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CVI.Domain.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<T> First(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<List<T>> FindAll(params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> ExecuteFromSqlRaw(string ps, object[] parameters);

        Task<IEnumerable<T>> ExecutePsAsync(string ps, IDictionary<string, object> parms);

        void Attach(T entity);

        void Detache(T entity);

        Task<T> CreateAsync(T entity);

        Task<IList<T>> CreateRangeAsync(IList<T> entities);

        Task<T> UpdateAsync(T entity, object key);

        Task<int> DeleteAsync(T entity);
    }
}
