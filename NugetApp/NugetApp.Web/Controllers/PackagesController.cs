using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NugetApp.Web.Controllers
{
    public class PackagesController : Controller
    {
        // GET: Package
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPackageBySearchText()
        {
            return View();
        }
    }
}