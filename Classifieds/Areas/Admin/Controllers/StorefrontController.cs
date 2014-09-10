using Classifieds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using Classifieds.Areas.Admin.Models.ViewModels;

namespace Classifieds.Areas.Admin.Controllers
{
    public class StorefrontController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        int pageSize = 10;
        public ActionResult Index(int page=1)
        {
            var storesQ = from s in db.Storefronts
                         select s;
            var model = storesQ.ToPagedList(page, pageSize);
            return View(model);
        }
        public ActionResult create()
        {
            return View(new CreateStorefrontViewModel());
        }
        [HttpPost]
        public ActionResult create(CreateStorefrontViewModel model)
        {
            return View();
        }
    }
}