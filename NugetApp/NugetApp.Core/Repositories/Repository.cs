using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.Repositories
{
    public class Repository<T> : IRepository<T> 
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public virtual async Task Create(T entity)
        {
            await _session.SaveAsync(entity);
        }

        public virtual async Task Update(T entity)
        {
            await _session.UpdateAsync(entity);
        }

        public virtual async Task Delete(int id)
        {
            var entity = await Get(id);
            await Delete(entity);
        }

        public virtual async Task Delete(T entity)
        {
            await _session.DeleteAsync(entity);
        }

        public virtual async Task<T> Get(int id)
        {
            return (await _session.GetAsync<T>(id));
        }

        public virtual IList<T> Get()
        {
            return _session.Query<T>().ToList();
        }

        public virtual int GetCount(Expression<Func<T, bool>> predicate = null)
        {
            var count = _session.Query<T>()
                .Where(predicate)
                .Count();

            return count;
        }

        public virtual IList<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            var list = _session.Query<T>()
                .Where(predicate)
                .ToList();

            return list;
        }
    }
}
