using NugetApp.Core.Services;
using Autofac;
using System.Web.Mvc;
using NugetApp.Core.Entities;
using System;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace NugetApp.Web.Models.PackageModels
{
    public class PackageUploadModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        //Microsoft.Web.Mvc.FileExtensions(Extensions = "csv",
        //     ErrorMessage = "Specify a CSV file. (Comma-separated values)")]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.zip|.exe|.nupkg)$", ErrorMessage = "Only Zip files allowed.")]
        public HttpPostedFileBase File { get; set; }

        private readonly IPackageService _packageService;

        private ApplicationUserManager _applicationUserManager;

        public PackageUploadModel()
        {
            _packageService = DependencyResolver.Current.GetService<IPackageService>();
            _applicationUserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public async Task Create(string userName)
        {
            var user = await _applicationUserManager.FindByEmailAsync(userName);

            if (user == null) throw new InvalidOperationException("User cannot be null.");

            var package = new Package
            {
                Name = Name,
                PackageDownloadCount = 0,
                ApplicationUser = user,
                Description = Description,
                LastPackageVersion = Version,
                LastUpdatedAt = DateTime.Now
            };

            var filePath = StoreFile();

            var packageVersion = new PackageVersion
            {
                VersionDownloadCount = 0,
                CreatedAt = DateTime.Now,
                VersionNumber = Version,
                FilePath = filePath,
                Package = package
            };

            await _packageService.Upload(package,packageVersion);
        }

        public async Task CreateNewVersion(string userName)
        {
            var user = await _applicationUserManager.FindByEmailAsync(userName);

            if (user == null) throw new InvalidOperationException("User cannot be null.");

            var package = await _packageService.GetPackageById(Id);
            package.ApplicationUser = user;
            package.Description = Description;
            package.LastPackageVersion = Version;
            package.LastUpdatedAt = DateTime.Now;

            var filePath = StoreFile();

            var packageVersion = new PackageVersion
            {
                VersionDownloadCount = 0,
                CreatedAt = DateTime.Now,
                VersionNumber = Version,
                FilePath = filePath,
                Package = package
            };

            await _packageService.UploadNewVersion(package,packageVersion);
        }

        public async Task LoadPackageData(int  pacakgeId)
        {
            var package = await _packageService.GetPackageById(pacakgeId);

            if (package == null) throw new Exception("Error occured in getting package");

            Id = package.Id;
            Name = package.Name;
            Description = package.Description;
        }

        private string StoreFile()
        {
            string filePath;
            var uploadDir = "~/Uploads";
            var path = Path.Combine(HttpContext.Current.Server.MapPath(uploadDir));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var postedFile = File;

            if (postedFile != null)
            {
                filePath = Path.Combine(path, postedFile.FileName);
                postedFile.SaveAs(filePath);
            }
            else
            {
                throw new Exception("File cannot be null.");
            }

            return filePath;
        }
    }
}