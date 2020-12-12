using NugetApp.Core.Entities;

namespace NugetApp.Web.Models.PackageModels
{
    public class PackageModel
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public long TotalDowloadCount { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}