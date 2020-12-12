using NugetApp.Core.DTOs;
using NugetApp.Core.Entities;
using NugetApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NugetApp.Web.Models.PackageModels
{
    public class PackageDetailsModel
    {
        private readonly IPackageService _packageService;

        public string Name { get; set; }
        public long PackageDownloadCount { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IList<PackageVersionDetailsModel> PackageVersionDetails { get; set; }

        public PackageDetailsModel()
        {
            _packageService = DependencyResolver.Current.GetService<IPackageService>();
        }
        public async Task GetPackageDetails(int id)
        {
            var package = await _packageService.GetPackageDetails(id);
            PopulatePackageDetails(package);
        }

        private void PopulatePackageDetails(PackageDTO packageDTO)
        {
            Name = packageDTO.Name;
            PackageDownloadCount = packageDTO.PackageDownloadCount;
            ApplicationUser = packageDTO.ApplicationUser;
            PackageVersionDetails = new List<PackageVersionDetailsModel>();

            foreach(var item in packageDTO.PackagerVersions)
            {
                var packageVersion = new PackageVersionDetailsModel();
                packageVersion.CreatedAt = item.CreatedAt;
                packageVersion.FilePath = item.FilePath;
                packageVersion.VersionDownloadCount = item.VersionDownloadCount;
                packageVersion.VersionNumber = item.VersionNumber;
                PackageVersionDetails.Add(packageVersion);
            }

        }
    }
}