using NugetApp.Core.Entities;
using NugetApp.Core.Services;
using NugetApp.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NugetApp.Web.Models.PackageModels
{
    public class PackageViewModel
    {
        private readonly IPackageService _packageService;
        public IList<Package> PackageDetails { get; set; }
        public PackageViewModel()
        {
            _packageService = DependencyResolver.Current.GetService<IPackageService>();
        }

        public void GetAllPackages()
        {
           PackageDetails =  _packageService.GetAllPackages();
        }

        public void GetPackageDetails(int id)
        {
            _packageService.GetPackageDetails(id);
        }
    }
}