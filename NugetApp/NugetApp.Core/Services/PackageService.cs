using NugetApp.Core.Entities;
using NugetApp.Core.UnitofWorks;
using NHibernate;
using System.Collections.Generic;
using System.Threading.Tasks;
using NugetApp.Core.DTOs;
using System;

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

        public IList<Package> GetPackagesOfUserId(ApplicationUser user)
        {
            var list = _packageUnitofWork.PackageRepository.Get(x => x.ApplicationUser.Id == user.Id);

            return list;
        }

        public async Task UploadFile()
        {

        }

        public async Task<Package> GetPackageById(int packageId)
        {
            var package = await _packageUnitofWork.PackageRepository.Get(packageId);
            return package;
        }

        public async Task<PackageDTO> GetPackageDetails(int id)
        {
            var package = await _packageUnitofWork.PackageRepository.Get(id);

            if (package == null) throw new NullReferenceException("Package not found");

            var packageDTO = new PackageDTO
            {
                Id = package.Id,
                ApplicationUser = package.ApplicationUser,
                Name = package.Name,
                PackageDownloadCount = package.PackageDownloadCount,
                PackageVersions = new List<PackageVersionDTO>()
            };

            foreach(var item in package.PackageVersions)
            {
                var packageVesion = new PackageVersionDTO
                {
                    CreatedAt = item.CreatedAt,
                    FilePath = item.FilePath,
                    VersionDownloadCount = item.VersionDownloadCount,
                    VersionNumber = item.VersionNumber
                };
                packageDTO.PackageVersions.Add(packageVesion);
            }

            return packageDTO;
        }

        public async Task UploadNewVersion(Package package)
        {
            await _packageUnitofWork.PackageRepository.Update(package);
        }
    }
}
