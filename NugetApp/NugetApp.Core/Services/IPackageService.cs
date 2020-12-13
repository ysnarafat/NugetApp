using NugetApp.Core.DTOs;
using NugetApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.Services
{
    public interface IPackageService
    {
        Task Upload(Package package);
        Task Delete(int id);
        Task UpdateDownloadCount(Package package, PackageVersion packageVersion);
        IList<Package> GetAllPackages();
        Task<PackageDTO> GetPackageDetails(int id);
        Task<Package> GetPackageById(int packageId);
        Task<PackageVersion> GetPackageVersionById(int packageVersionId);
        IList<Package> GetPackagesOfUserId(ApplicationUser user);
        Task UploadNewVersion(Package package, PackageVersion packageVersion);
    } 
}
