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
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public HttpPostedFileBase file { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

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

            var package = new Package
            {
                Name = Name,
                PackageDownloadCount = 0,
                ApplicationUser = user,
            };

            package.PackageVersions = new List<PackageVersion>
            {
                    new PackageVersion
                    {
                        Description = "Test",
                        VersionDownloadCount = 0,
                        CreatedAt = DateTime.Now,
                        Package = package
                    }
            };

            //Path.G
            //if (!Directory.Exists(Path.Combine("~/UploadedFiles", "test")))
            //{
            //    Directory.CreateDirectory(Path.Combine("~/UploadedFiles", "test"));
            //}

            //var path = Path.Combine("~/UploadedFiles", "test");

            //if (!File.Exists(path))
            //{
            //    var profileImage = File.OpenWrite(path);
            //    //var uploadFile = File.OpenReadStream();
            //    //uploadFile.CopyTo(profileImage);
            //}

            var uploadDir = "~/Uploads";
            var path = Path.Combine(HttpContext.Current.Server.MapPath(uploadDir));
            //string path = Path.Combine("~/Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var postedFile = file;

            if (postedFile != null)
            {
                string filePath = Path.Combine(path,postedFile.FileName);
                postedFile.SaveAs(filePath);
                //ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
            }

            _packageService.Upload(package);
        }

        public async Task Delete(int  userName)
        {
            //var user = await _applicationUserManager.FindByEmailAsync(userName);

            //var package = new Package
            //{
            //    Name = Name,
            //    PackageDownloadCount = 0,
            //    ApplicationUser = user,
            //};

            //package.PackageVersions = new List<PackageVersion>
            //{
            //        new PackageVersion
            //        {
            //            Description = "Test",
            //            VersionDownloadCount = 0,
            //            CreatedAt = DateTime.Now,
            //            Package = package
            //        }
            //};

            _packageService.Delete(userName);
        }
    }
}