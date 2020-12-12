using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.DTOs
{
    public class PackageVersionDTO
    {
        public virtual string VersionNumber { get; set; }
        public virtual string FilePath { get; set; }
        public virtual long VersionDownloadCount { get; set; }
        public virtual DateTime CreatedAt { get; set; }
    }
}
