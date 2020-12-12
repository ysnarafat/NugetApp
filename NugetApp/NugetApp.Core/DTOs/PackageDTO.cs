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
        public virtual string Name { get; set; }
        public virtual long PackageDownloadCount { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public IList<PackageVersionDTO> PackagerVersions { get; set; }
    }
}
