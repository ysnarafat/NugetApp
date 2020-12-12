using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.Repositories
{
    public interface IRepository<T>
    {
        IList<T> Get();
        IList<T> Get(Expression<Func<T, bool>> predicate = null);
        int GetCount(Expression<Func<T, bool>> predicate = null);
        Task<T> Get(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task Delete(int id);
    }
}
