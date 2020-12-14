using NugetApp.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NugetApp.Web.Models.PackageModels
{
    public class PackageDownloadModel
    {
        private readonly IPackageService _packageService;

        public string FilePath { get; set; }
        public string FileName { get; set; }
        public PackageDownloadModel()
        {
            _packageService = DependencyResolver.Current.GetService<IPackageService>();
        }

        public async Task IncrementDownloadCount(int packageVersionId)
        {
            var packageVersion = await _packageService.GetPackageVersionById(packageVersionId);
            FilePath = packageVersion.FilePath;
            FileName = Path.GetFileName(FilePath);
            if (File.Exists(FilePath))
            {
                packageVersion.VersionDownloadCount++;

                var package = await _packageService.GetPackageById(packageVersion.Package.Id);
                package.PackageDownloadCount++;

                await _packageService.UpdateDownloadCount(package, packageVersion);
            }
            else
            {
                throw new Exception("Error while gettng file.");
            }
            
        }
    }
}