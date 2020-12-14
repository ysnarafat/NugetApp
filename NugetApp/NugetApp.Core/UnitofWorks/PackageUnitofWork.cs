using NugetApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.UnitofWorks
{
    public class PackageUnitofWork : UnitofWork , IPackageUnitofWork
    {
        public IPackageRepository PackageRepository { get ; set ; }
        public IPackageVersionRepository PackageVersionRepository { get; set; }
        public PackageUnitofWork(IPackageRepository packageRepository,
        IPackageVersionRepository packageVersionRepository)
        {
            PackageRepository = packageRepository;
            PackageVersionRepository = packageVersionRepository;
        }
    }
}
