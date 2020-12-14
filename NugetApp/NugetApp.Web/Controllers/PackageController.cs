using NugetApp.Web.Models.PackageModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(PackageUploadModel model)
        {
            try
            {
                await model.Create(User.Identity.Name);
                TempData["SuccessNotify"] = "Successfully uploaded your package";
            }
            catch
            {
                TempData["ErrorNotify"] = "An error occured while uploading package";
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> UploadNewVersion(int id)
        {
            var model = new PackageUploadModel();
            try
            {
                await model.LoadPackageData(id);
                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadNewVersion(PackageUploadModel model)
        {
            try
            {
                await model.CreateNewVersion(User.Identity.Name);
                TempData["SuccessNotify"] = "Successfully uploaded your package";
                return RedirectToAction("Details", new RouteValueDictionary( new { id = model.Id }));
            }
            catch
            {
                TempData["ErrorNotify"] = "An error occured while uploading package";
            }

            return View(model);
        }

        [HttpGet]
        public virtual async Task<ActionResult> Download(int id)
        {
            var model = new PackageDownloadModel();
            try
            {
                await model.IncrementDownloadCount(id);
                return File(model.FilePath, MimeMapping.GetMimeMapping(model.FileName), model.FileName);
                //return View();
            }
            catch
            {
                return View("Error");
            }
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

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var model = new PackageDetailsModel();
            try
            {
                await model.GetPackageDetails(id);

                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }
    }
}