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
        // GET: Upload
        public ActionResult Index()
        {
            var model = new PackageViewModel();
            model.GetAllPackages();
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Upload()
        {
            var model = new PackageUploadModel();
            //await model.Create(User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Upload(PackageUploadModel model)
        {
            var name = model.file.FileName;
            Console.WriteLine(name);
            await model.Create(User.Identity.Name);
            return View(model);
        }

        [HttpGet]
        public void Delete()
        {
            var model = new PackageUploadModel();
            
        }

        [HttpPost]
        public void Delete(int id)
        {
            var model = new PackageUploadModel();
            model.Delete(id);
        }

        [HttpGet]
        public void Details(int id)
        {
            var model = PackageViewModel();
            model.

        }


    }
}