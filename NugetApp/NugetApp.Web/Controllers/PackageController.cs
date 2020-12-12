using NugetApp.Web.Models.PackageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NugetApp.Web.Controllers
{
    public class PackageController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new PackageViewModel();
            model.GetAllPackages();

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Upload()
        {
            var model = new PackageUploadModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Upload(PackageUploadModel model)
        {
            try
            {
                await model.Create(User.Identity.Name);
                TempData["SuccessNotify"] = "Successfully uploaded your pacakage";
            }
            catch
            {
                TempData["ErrorNotify"] = "An error occured while uploading pacakage";
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public void Delete()
        {
            var model = new PackageUploadModel();
            
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> MyPackages()
        {
            var model = new PackageViewModel();
            try
            {
                await model.GetPackageByUser(User.Identity.Name);
            }
            catch
            {
                return View("Error");
            }

            return View(model);
        }

        [HttpPost]
        public void Delete(int id)
        {
            var model = new PackageUploadModel();
            model.Delete(id);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var model = new PackageDetailsModel();
            await model.GetPackageDetails(id);

            return View(model);
        }
    }
}