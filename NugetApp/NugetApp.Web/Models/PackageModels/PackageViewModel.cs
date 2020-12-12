using Microsoft.AspNet.Identity.Owin;
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
    public class PackageViewModel
    {
        public IList<PackageModel> Packages { get; set; }
        ///public IList<Package> Packages { get; set; }

        private readonly IPackageService _packageService;

        private ApplicationUserManager _applicationUserManager;

        public PackageViewModel()
        {
            _packageService = DependencyResolver.Current.GetService<IPackageService>();
            _applicationUserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            Packages = new List<PackageModel>();
        }

        public void GetAllPackages()
        {
           var packageList =  _packageService.GetAllPackages();


            //Packages = packageList;
            foreach (var package in packageList)
            {
                Packages.Add
                (
                    new PackageModel
                    {
                        Description = package.Description,
                        Id = package.Id,
                        PackageName = package.Name,
                        TotalDowloadCount = package.PackageDownloadCount,
                        ApplicationUser = package.ApplicationUser
                    }
                );
            }
        }

        public async Task GetPackageByUser(string  userName)
        {
            var user = await _applicationUserManager.FindByEmailAsync(userName);

            if (user == null) throw new InvalidOperationException("User cannot be null.");

            var packageList = _packageService.GetPackagesOfUserId(user);
            //Packages = packageList;
            foreach (var package in packageList)
            {
                Packages.Add
                (
                    new PackageModel
                    {
                        Description = package.Description,
                        Id = package.Id,
                        PackageName = package.Name,
                        TotalDowloadCount = package.PackageDownloadCount,
                        ApplicationUser = package.ApplicationUser
                    }
                );
            }
        }
    }
}