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
        [Required]
        //Microsoft.Web.Mvc.FileExtensions(Extensions = "csv",
        //     ErrorMessage = "Specify a CSV file. (Comma-separated values)")]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.zip|.exe|.nupkg)$", ErrorMessage = "Only Zip files allowed.")]
        public HttpPostedFileBase File { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Version { get; set; }

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
                Description = Description
            };

            var filePath = StoreFile();

            package.PackageVersions = new List<PackageVersion>
            {
                new PackageVersion
                {
                    VersionDownloadCount = 0,
                    CreatedAt = DateTime.Now.Date,
                    VersionNumber = Version,
                    FilePath = filePath,
                    Package = package
                }
            };

            await _packageService.Upload(package);
        }

        //public async Task Delete(int userName)
        //{
        //    //var user = await _applicationUserManager.FindByEmailAsync(userName);

        //    //var package = new Package
        //    //{
        //    //    Name = Name,
        //    //    PackageDownloadCount = 0,
        //    //    ApplicationUser = user,
        //    //};

        //    //package.PackageVersions = new List<PackageVersion>
        //    //{
        //    //        new PackageVersion
        //    //        {
        //    //            Description = "Test",
        //    //            VersionDownloadCount = 0,
        //    //            CreatedAt = DateTime.Now,
        //    //            Package = package
        //    //        }
        //    //};

        //    _packageService.Delete(userName);
        //}

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