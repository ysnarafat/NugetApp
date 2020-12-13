using NugetApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.DTOs
{
    public class PackageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long PackageDownloadCount { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IList<PackageVersionDTO> PackageVersions { get; set; }
    }
}
