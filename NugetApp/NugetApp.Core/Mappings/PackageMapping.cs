using FluentNHibernate.Mapping;
using NugetApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetApp.Core.Mappings
{
    public class PackageMapping : ClassMap<Package>
    {
        public PackageMapping()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Name);
            Map(p => p.Description).Length(100000);
            Map(p => p.LastPackageVersion);
            Map(p => p.LastUpdatedAt);
            Map(p => p.PackageDownloadCount);
            References(p => p.ApplicationUser);
            HasMany(p => p.PackageVersions);
        }
    }
}
