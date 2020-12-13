using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NugetApp.Web.Models.PackageModels
{
    public class PackageVersionDetailsModel
    {
        public int Id { get; set; }
        public string VersionNumber { get; set; }
        public string FilePath { get; set; }
        public long VersionDownloadCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}