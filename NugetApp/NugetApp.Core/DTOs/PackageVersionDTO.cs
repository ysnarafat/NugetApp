using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.DTOs
{
    public class PackageVersionDTO
    {
        public int Id { get; set; }
        public string VersionNumber { get; set; }
        public string FilePath { get; set; }
        public long VersionDownloadCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
