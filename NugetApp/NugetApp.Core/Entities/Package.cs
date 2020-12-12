using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.Entities
{
    public class Package
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual long PackageDownloadCount { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<PackageVersion> PackageVersions { get; set; }
    }
}
