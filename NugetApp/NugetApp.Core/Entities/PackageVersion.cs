using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.Entities
{
    public class PackageVersion
    {
        public virtual int Id { get; set; }
        public virtual string VersionNumber { get; set; }
        public virtual string FilePath { get; set; }
        public virtual long VersionDownloadCount { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual Package Package { get; set; }
    }
}
