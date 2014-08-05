using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Classifieds.Models.SearchViewModels;
using Classifieds.Models.ViewModels;
using Classifieds.Models;
using Classifieds.Library;

namespace Classifieds.Controllers
{
    public class BrowseController : Controller
    {
        CategoryHelper catHelper = new CategoryHelper();
        

        public ActionResult Index([ModelBinder(typeof(ClassifiedsSearchModelBinder))] AListingSearchModel model)
        {
            
            //trigger searching for listings with submitted model
            model.findListings();
            //used for passing this model to layout file to pass on to partial views
            ViewBag.searchModel = model;
              ViewBag.crumbs = catHelper.categoryParents(model.cat);
            return View(model.view,model.layout,model);
        }
    }
}