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
        void GetPackageDetails(int id);
    } 
}
