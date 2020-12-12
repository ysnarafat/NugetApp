using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.UnitofWorks
{
    public interface IUnitofWork
    {
        void Commit();
        void Rollback();
        void BeginTransaction(ISession session);
    }
}
