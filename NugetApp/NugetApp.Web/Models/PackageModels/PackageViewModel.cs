using Microsoft.AspNet.Identity.Owin;
using NugetApp.Core.Entities;
using NugetApp.Core.Services;
using NugetApp.Web.DTOs;
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
        public IList<Package> PackageDetails { get; set; }

        private readonly IPackageService _packageService;

        private ApplicationUserManager _applicationUserManager;

        public PackageViewModel()
        {
            _packageService = DependencyResolver.Current.GetService<IPackageService>();
            _applicationUserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public void GetAllPackages()
        {
           PackageDetails =  _packageService.GetAllPackages();
        }

        public async Task GetPackageByUser(string  userName)
        {
            var user = await _applicationUserManager.FindByEmailAsync(userName);

            PackageDetails = _packageService.GetPackagesOfUserId(user);
        }
    }
}