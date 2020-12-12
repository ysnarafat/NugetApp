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
        IList<Package> GetAllPackages();
        Task<PackageDTO> GetPackageDetails(int id);
        IList<Package> GetPackagesOfUserId(ApplicationUser user);
    } 
}
