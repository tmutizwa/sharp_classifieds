using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Classifieds.Areas.Admin.Controllers
{
    public class DealsController : Controller
    {
        // GET: Admin/Deals
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult create()
        {
            
            return View();
        }

    }
}