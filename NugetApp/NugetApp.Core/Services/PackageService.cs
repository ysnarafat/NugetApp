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
        public PackageService(IPackageUnitofWork packageUnitofWork,ISession session)
        {
            _packageUnitofWork = packageUnitofWork;
            _session = session;
        }
        public async Task Upload(Package package,PackageVersion packageVersion)
        {
            await _packageUnitofWork.PackageRepository.Create(package);
            await _packageUnitofWork.PackageVersionRepository.Create(packageVersion);
        }

        public async Task Delete(int id)
        {
            _packageUnitofWork.BeginTransaction(_session);
            try
            {
                await _packageUnitofWork.PackageRepository.Delete(id);
                _packageUnitofWork.Commit();
            }
            catch
            {
                _packageUnitofWork.Rollback();
            }

        }

        public IList<PackageDTO> GetAllPackages()
        {
            var list = _packageUnitofWork.PackageRepository.Get();
            var packageList = new List<PackageDTO>();
            foreach(var item in list)
            {
                packageList.Add(new PackageDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    ApplicationUser = item.ApplicationUser,
                    Description = item.Description,
                    LastPackageVersion = item.LastPackageVersion,
                    LastUpdatedAt = item.LastUpdatedAt,
                    PackageDownloadCount = item.PackageDownloadCount
                });
            }

            return packageList;
        }

        public IList<Package> GetPackagesOfUserId(ApplicationUser user)
        {
            var list = _packageUnitofWork.PackageRepository.Get(x => x.ApplicationUser.Id == user.Id);

            return list;
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
                Description = package.Description,
                PackageDownloadCount = package.PackageDownloadCount,
                LastPackageVersion = package.LastPackageVersion,
                LastUpdatedAt = package.LastUpdatedAt,
                PackageVersions = new List<PackageVersionDTO>()
            };

            foreach(var item in package.PackageVersions)
            {
                var packageVesion = new PackageVersionDTO
                {
                    Id = item.Id,
                    CreatedAt = item.CreatedAt,
                    FilePath = item.FilePath,
                    VersionDownloadCount = item.VersionDownloadCount,
                    VersionNumber = item.VersionNumber
                };
                packageDTO.PackageVersions.Add(packageVesion);
            }

            return packageDTO;
        }

        public async Task UploadNewVersion(Package package,PackageVersion packageVersion)
        {
            _packageUnitofWork.BeginTransaction(_session);
            try
            {
                await _packageUnitofWork.PackageRepository.Update(package);
                await _packageUnitofWork.PackageVersionRepository.Create(packageVersion);
                _packageUnitofWork.Commit();
            }
            catch
            {
                _packageUnitofWork.Rollback();
            }
        }

        public async Task<PackageVersion> GetPackageVersionById(int packageVersionId)
        {
            var packageVersion = await _packageUnitofWork.PackageVersionRepository.Get(packageVersionId);
            return packageVersion;
        }

        public async Task UpdateDownloadCount(Package package, PackageVersion packageVersion)
        {
            _packageUnitofWork.BeginTransaction(_session);
            try
            {
                await _packageUnitofWork.PackageRepository.Update(package);
                await _packageUnitofWork.PackageVersionRepository.Update(packageVersion);
                _packageUnitofWork.Commit();
            }
            catch
            {
                _packageUnitofWork.Rollback();
            }
        }
    }
}
