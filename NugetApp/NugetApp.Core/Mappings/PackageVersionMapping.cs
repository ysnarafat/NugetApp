using FluentNHibernate.Mapping;
using NugetApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.Mappings
{
    public class PackageVersionMapping : ClassMap<PackageVersion>
    {
        public PackageVersionMapping()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Description);
            Map(p => p.VersionNumber);
            Map(p => p.FilePath);
            Map(p => p.VersionDownloadCount);
            Map(p => p.CreatedAt);
            References(p => p.Package);
        }
    }
}
