using CVI.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace CVI.Infrastructure.Repository
{
    /// <summary>
    /// The GenericRepository class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        /// <summary>
        /// The context
        /// </summary>
        protected CviContext _context;

        /// <summary>
        /// Context
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(CviContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Find 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return await includes.Aggregate(_context.Set<T>().Where(predicate), (current, includeProperty) => current.Include(includeProperty)).ToListAsync();
        }

        /// <summary>
        /// First 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<T> First(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return await includes.Aggregate(_context.Set<T>().Where(predicate), (current, includeProperty) => current.Include(includeProperty)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Find all 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<T>> FindAll(params Expression<Func<T, object>>[] includes)
        {
            return await includes.Aggregate(_context.Set<T>().AsQueryable(), (current, includeProperty) => current.Include(includeProperty)).ToListAsync();
        }

        /// <summary>
        /// Excute procedure. 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> ExecuteFromSqlRaw(string ps, object[] parameters)
        {
            return await _context.Set<T>().FromSqlRaw(ps, parameters).ToListAsync();
        }

        /// <summary>
        /// Excute procedure with params. 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> ExecutePsAsync(string ps, IDictionary<string, object> parms)
        {
            var cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ps;

            Parallel.ForEach(parms, (cParam) =>
            {
                var param = cmd.CreateParameter();
                param.ParameterName = cParam.Key;
                param.Value = cParam.Value;
                cmd.Parameters.Add(param);
            });

            using (cmd)
            {
                if (cmd.Connection.State == ConnectionState.Closed) { await cmd.Connection.OpenAsync(); }
                try
                {
                    using var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess);
                    return MapToList<T>(reader);
                }
                catch
                {
                    return null;
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
        }

        /// <summary>
        /// entity to attach
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void Attach(T entity)
        {
            _context.Attach(entity);
        }

        /// <summary>
        /// entity to attach
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void Detache(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<IList<T>> CreateRangeAsync(IList<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<T> UpdateAsync(T entity, object key)
        {
            if (entity == null) return null;
            T exist = await _context.Set<T>().FindAsync(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
            }
            return exist;
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        #region Private methods

        /// <summary>
        /// Map data to list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        private IEnumerable<T> MapToList<T>(DbDataReader dr)
        {
            var props = typeof(T).GetRuntimeProperties();
            var objList = new List<T>();

            var colMapping = dr.GetColumnSchema()
              .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
              .ToDictionary(key => key.ColumnName.ToLower());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (var prop in props)
                    {
                        if (colMapping.Keys.Contains(prop.Name.ToLower()))
                        {
                            var val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                            prop.SetValue(obj, val == DBNull.Value ? null : val);
                        }
                    }
                    objList.Add(obj);
                }
            }
            return objList;
        }

        #endregion
    }
}
