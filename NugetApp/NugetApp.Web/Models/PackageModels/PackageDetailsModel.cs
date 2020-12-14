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

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long PackageDownloadCount { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string LastPackageVersion { get; set; }
        public string LastUpdatedAt { get; set; }
        public IList<PackageVersionDetailsModel> PackageVersionDetails { get; set; }

        public PackageDetailsModel()
        {
            _packageService = DependencyResolver.Current.GetService<IPackageService>();
        }

        public async Task GetPackageDetails(int id)
        {
            var package = await _packageService.GetPackageDetails(id);
            if (package == null) throw new Exception("Package not found");

            PopulatePackageDetails(package);
        }

        private void PopulatePackageDetails(PackageDTO packageDTO)
        {
            Id = packageDTO.Id;
            Name = packageDTO.Name;
            Description = packageDTO.Description;
            PackageDownloadCount = packageDTO.PackageDownloadCount;
            ApplicationUser = packageDTO.ApplicationUser;
            LastPackageVersion = packageDTO.LastPackageVersion;
            LastUpdatedAt = packageDTO.LastUpdatedAt.ToString("d");
            PackageVersionDetails = new List<PackageVersionDetailsModel>();

            foreach(var item in packageDTO.PackageVersions)
            {
                var packageVersion = new PackageVersionDetailsModel();
                packageVersion.Id = item.Id;
                packageVersion.CreatedAt = item.CreatedAt;
                packageVersion.FilePath = item.FilePath;
                packageVersion.VersionDownloadCount = item.VersionDownloadCount;
                packageVersion.VersionNumber = item.VersionNumber;
                PackageVersionDetails.Add(packageVersion);
            }

            PackageVersionDetails = PackageVersionDetails.OrderByDescending(x => x.CreatedAt).ToList();
        }
    }
}