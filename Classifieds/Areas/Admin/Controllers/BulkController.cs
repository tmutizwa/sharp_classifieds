using Classifieds.Areas.Admin.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Classifieds.Areas.Admin.Controllers
{
    public class BulkController : Controller
    {
        // GET: Admin/Bulk
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult upload()
        {
            
            return View(new BulkUploadC4Plus());
        }
        [HttpPost]
        public ActionResult upload(BulkUploadC4Plus model)
        {
            if (ModelState.IsValid)
            {
                Dictionary<string, string>[] ads = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(model.Text);
                foreach (var ad in ads)
                {
                    Console.WriteLine("Pano");
                }
            }
            return View(model);
        }
    }
}