using NugetApp.Core.Entities;
using NugetApp.Core.UnitofWorks;
using NHibernate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NugetApp.Core.Services
{
    public class PackageService : IPackageService
    {
        private IPackageUnitofWork _packageUnitofWork;

        private readonly ISession _session;
        public PackageService(IPackageUnitofWork packageUnitofWork, ISession session)
        {
            _packageUnitofWork = packageUnitofWork;
            _session = session;
        }
        public async Task Upload(Package package)
        {
            await _packageUnitofWork.PackageRepository.Create(package);
        }

        public async Task Delete(int id)
        {
            await _packageUnitofWork.PackageRepository.Delete(id);
        }

        public IList<Package> GetAllPackages()
        {
            //_session.Clear();

            var list = _packageUnitofWork.PackageRepository.Get();
            return list;
        }

        public async Task UploadFile()
        {

        }

        public void GetPackageDetails(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
