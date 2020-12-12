using NugetApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.UnitofWorks
{
    public interface IPackageUnitofWork : IUnitofWork
    {
        IPackageRepository PackageRepository { get; set; }

    }
}
