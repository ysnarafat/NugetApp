using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NugetApp.Web.DTOs
{
    public class PackageDetails
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set;}
        public DateTime CreatedAt { get; set; }
        public string Downloads { get; set; }

    }
}