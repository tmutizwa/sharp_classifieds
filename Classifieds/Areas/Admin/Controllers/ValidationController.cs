using Classifieds.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Classifieds.Areas.Admin.Controllers
{
    //public class ValidationController : Controller
    //{
    //    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    //    public ActionResult IsCategoryNameAvailable(string Name)
    //    {
    //        using (ApplicationDbContext db = new ApplicationDbContext())
    //        {
    //            try
    //            {
    //                var tag = db.Categories.Single(m => m.Name == Name);
    //                return Json(false, JsonRequestBehavior.AllowGet);
    //            }
    //            catch (Exception)
    //            {
    //                return Json(true, JsonRequestBehavior.AllowGet);
    //            }
    //        }
    //    }

    //}
}