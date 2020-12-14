using NHibernate;
using NugetApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.Repositories
{
    public class PackageVersionRepository : Repository<PackageVersion>, IPackageVersionRepository
    {
        public PackageVersionRepository(ISession session) : base(session)
        {

        }
    }
}
