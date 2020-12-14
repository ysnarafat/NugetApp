using NugetApp.Core.Entities;
using System;

namespace NugetApp.Web.Models.PackageModels
{
    public class PackageModel
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public long TotalDowloadCount { get; set; }
        public string LastPackageVersion { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}