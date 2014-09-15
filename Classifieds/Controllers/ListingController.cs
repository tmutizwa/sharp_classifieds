using Classifieds.Areas.Admin.Models;
using Classifieds.Library;
using Classifieds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Classifieds.Models.SearchViewModels;
using PagedList;
using System.Net;
using System.IO;
using System.Data.Entity;
using LinqKit;
using Classifieds.Models.CreateViewModels;
using Classifieds.Models.EditViewModels;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Classifieds.Models.ViewModels;

namespace Classifieds.Controllers
{
    [Authorize]
    public class ListingController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        CategoryHelper categoryHelper = new CategoryHelper();
        int pagesize = 10;
        int maxImages = 9;  
        
        public ActionResult index(string order, string ctitle, string category, string status, int? page)
        {
            string userId = User.Identity.GetUserId();
            var listings = from l in db.Listings.Include("Category").Include("images")
                           where (l.OwnerId == userId)
                           select l;    

            if (!String.IsNullOrEmpty(ctitle))
            {
                listings = listings.Where(l => l.Title.ToLower().Contains(ctitle.Trim().ToLower()));
            }
            if (!String.IsNullOrEmpty(category))
            {
                listings = listings.Where(l => l.Category.Title.ToLower().Contains(category.Trim().ToLower()));
            }
            if (!String.IsNullOrEmpty(status))
            {
                listings = listings.Where(l => l.Status.ToLower().Contains(status.Trim().ToLower()));
            }
            string titleOrder = "title_desc";
            string categoryOrder = "category";
            string statusOrder = "status";
            string expiresOrder = "expires";
            string priceOrder = "price";
            string updatedOrder = "updated_desc";

                switch(order){
                    case "title_desc":
                        listings = listings.OrderByDescending(l=>l.Title);
                        titleOrder = "title";
                        break;
                    case "title":
                        listings = listings.OrderBy(l=>l.Title);
                        titleOrder = "title_desc";
                        break;
                    case "status":
                        listings = listings.OrderBy(l=>l.Status);
                        statusOrder = "status_desc";
                        break;
                    case "status_desc":
                        listings = listings.OrderByDescending(l=>l.Status);
                        statusOrder = "status";
                        break;
                    case "category":
                        listings = listings.OrderBy(l => l.Category.Title);
                        categoryOrder = "category_desc";
                        break;
                    case "category_desc":
                        listings = listings.OrderByDescending(l=>l.Category.Title);
                        categoryOrder = "category";
                        break;
                    case "expires":
                        listings = listings.OrderBy(l=>l.Expires);
                        expiresOrder = "expires_desc";
                        break;
                    case "expires_desc":
                        listings = listings.OrderByDescending(l=>l.Expires);
                        expiresOrder = "expires";
                        break;
                    case "price":
                        listings = listings.OrderBy(l=>l.Price);
                        priceOrder = "price_desc";
                        break;
                    case "price_desc":
                        listings = listings.OrderByDescending(l=>l.Price);
                        priceOrder = "price";
                        break;
                    case "updated":
                        listings = listings.OrderBy(l=>l.Updated);
                        updatedOrder = "updated_desc";
                        break;
                    default:
                        listings = listings.OrderByDescending(l=>l.Updated);
                        updatedOrder = "updated";
                        break;
                }
            ViewBag.titleOrder = titleOrder;
            ViewBag.categoryOrder = categoryOrder;
            ViewBag.statusOrder = statusOrder;
            ViewBag.expiresOrder = expiresOrder;
            ViewBag.priceOrder = priceOrder;
            ViewBag.updatedOrder = updatedOrder;
            //Pagination
            // Set _page to the value of page if page is NOT null; otherwise _page,
           // int _page = page ?? 1;
            return View(listings.ToPagedList(1,pagesize));
        }
        public ActionResult choose(int c=0)
        {
            var cats = from cat in db.Categories
                       select cat;
            if (c > 0)
            {
                cats = cats.Where(t=>t.ParentId == c);
                ViewBag.crumbs = categoryHelper.categoryParents(c);
                //check if it's final child
                if(categoryHelper.isParentCategory(c) == false){
                    //redirect to internal/this controller action to confirm creation
                    return RedirectToAction("create" ,new { c=c});
                }
            }
            else
            {
                cats = cats.Where(t => t.ParentId == null);
            }
            cats = cats.OrderBy(t => t.Order).ThenBy(b=>b.Title);
            
            return View(cats);
        }
        public ActionResult create(int c)
        {
            if (c <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

           var catQ= from cat in db.Categories
                     where (cat.CategoryId == c)
                     select cat;
           Category category = catQ.FirstOrDefault();
           if (category == null)
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           if(categoryHelper.isParentCategory(category.CategoryId))
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           if (category.ViewModel != null)
           {
               var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
               Type modelType = Type.GetType("Classifieds.Models.CreateViewModels.Create" + category.ViewModel + ", " + assembly);
               if (modelType != null)
               {
                   object[] args = new object[]{c};
                   var instance = (ACreateListingViewModel)Activator.CreateInstance(modelType,args);
                   ViewBag.crumbs = categoryHelper.categoryParents(c);
                   return View(instance.viewFolder+instance.view,instance);
               }
               else throw new Exception("'Create model' for this category not found. Check file exists.");
               //return View();
           }
            throw new Exception("Create model for category not set. Check in DB.");
        }
        [HttpPost]
        public ActionResult create([ModelBinder(typeof(ClassifiedsCreateListingModelBinder))] ACreateListingViewModel model)
        {
            if (ModelState.IsValid)
            {
                int listingId = model.AddNewListing();
                if (listingId > 0)
                    return RedirectToAction("images", "listing", new { id=listingId});
                else
                {
                    ModelState.AddModelError("", "Error. Failed to save new listing. Please try again later or contact Administrator if problem persists.");
                }
                
            }
            
           // ModelState.AddModelError("", "Error. Failed to save new listing. Please try again later or contact Administrator if problem persists.");
            return View(model.viewFolder+model.view,model);
        }
        public ActionResult edit(int id)
        {
            if (id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            string userId = User.Identity.GetUserId();

            var listingQ = from l in db.Listings.Include("Category")
                           where l.ListingId == id && l.OwnerId == userId
                           select l;
            var listing = listingQ.FirstOrDefault();
            if (listing == null)
            {
                return new  HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                string viewmodel = listing.Category.ViewModel;
                var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                Type modelType = Type.GetType("Classifieds.Models.EditViewModels.Edit" + listing.Category.ViewModel + ", " + assembly);
                if (modelType != null)
                {
                    object[] args = new object[] { listing };
                    var instance = (AEditListingViewModel)Activator.CreateInstance(modelType, args);
                    ViewBag.crumbs = categoryHelper.categoryParents(listing.Category.CategoryId);
                    return View(instance.viewFolder + instance.view, instance);
                }
            }catch(Exception){
                throw new Exception("'View model' for this category not found.Check db config and that file exists.");
            }
            return View();

        }
        [HttpPost]
        public ActionResult edit([ModelBinder(typeof(ClassifiedsEditListingModelBinder))] AEditListingViewModel model)
        {
            if (ModelState.IsValid)
            {
                try{
                    model.EditListing();
                    return RedirectToAction("view", new { id=model.ListingId});
                }catch(DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            ModelState.AddModelError("", validationError.ErrorMessage);
                        }
                    }
                    //we need to reset the category since it's gone now from the form submission
                    model.Category = db.Categories.Find(model.CategoryId);
                }
            }
            return View(model.viewFolder + model.view, model);
        }
        public ActionResult view(int id)
        {
           string user =  User.Identity.GetUserId();
           var listingQ = from c in db.Listings.Include("Category").Include("images")
                          where c.ListingId == id && c.OwnerId == user
                          select c;
           if (listingQ == null || id <= 0)
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           Listing listing = listingQ.FirstOrDefault();
           if (!String.IsNullOrEmpty(listing.OwnerId) && listing != null)
           {
               if (String.IsNullOrEmpty(listing.Category.ViewModel))
                   throw new Exception("Viewmodel not set up for category.");
               //check the viewmodel to use
                   string model = listing.Category.ViewModel;
                   var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                   Type modelType = Type.GetType("Classifieds.Models.ViewModels." + model + ", " + assembly);
                   if (modelType != null)
                   {
                       object[] args = new object[] { listing };
                       var instance = (AListingViewModel)Activator.CreateInstance(modelType, args);
                       return View(instance.viewFolder + instance.view, instance);
                   }
                   else
                   {
                       throw new Exception("Check that model file exists.");
                   }
           }
           return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [AllowAnonymous]
        public ActionResult details(int id)
        {
            if (id <= 0 )
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var listingQ = from l in db.Listings.Include("Category").Include("images").Include("Owner").Include("Deal")
                           where l.ListingId == id 
                           select l;
            var listing = listingQ.FirstOrDefault();
            if (listing == null || listing.Category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            try
            {
                string viewmodel = listing.Category.ViewModel;
                var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                Type modelType = Type.GetType("Classifieds.Models.ViewModels." + listing.Category.ViewModel + ", " + assembly);
                if (modelType != null)
                {
                    object[] args = new object[] { listing };
                    var instance = (AListingViewModel)Activator.CreateInstance(modelType, args);
                    ViewBag.crumbs = categoryHelper.categoryParents(listing.Category.CategoryId);
                    ViewBag.searchViewModel = new ListingSearchViewModel();
                    ViewBag.viewModel = instance;
                    return View(instance.detailsFolder + instance.view, instance);
                }
            }
            catch (Exception)
            {
                throw new Exception("'View model' for this category not found.Check db config and that file exists.");
            }
            return View();
        }
        public ActionResult images(int id)
        {
            string user = User.Identity.GetUserId();
            var listingQ = from l in db.ListingImages.Include("Listing")
                           where l.ListingId == id && l.Listing.OwnerId == user
                           select l;
            var listingImages = listingQ.ToList();
            if (id <= 0|| listingImages == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Listings.Find(id);
            listing.images = listingImages;
           // Listing listing = new Listing();
            return View(listing);
        }
        public ActionResult delete(int id)
        {
            string user = User.Identity.GetUserId();
            var listingQ = from c in db.Listings.Include("Category").Include("images")
                           where c.ListingId == id && c.OwnerId == user
                           select c;
            if (listingQ == null || id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Listing listing = listingQ.FirstOrDefault();
            if (!String.IsNullOrEmpty(listing.OwnerId) && listing != null)
            {
                if (String.IsNullOrEmpty(listing.Category.ViewModel))
                    throw new Exception("Viewmodel not set up for category.");
                //check the viewmodel to use
                string model = listing.Category.ViewModel;
                var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                Type modelType = Type.GetType("Classifieds.Models.ViewModels." + model + ", " + assembly);
                if (modelType != null)
                {
                    object[] args = new object[] { listing };
                    var instance = (AListingViewModel)Activator.CreateInstance(modelType, args);
                    //ViewBag.crumbs = categoryHelper.categoryParents(id);
                    return View(instance.deleteFolder + "all.cshtml", instance);
                }
                else
                {
                    throw new Exception("Check that model file exists.");
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string user = User.Identity.GetUserId();
            var lQ = from l in db.Listings.Include("images")
                     where l.ListingId == id && l.OwnerId == user
                     select l;
            if (lQ == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Listing listing = lQ.FirstOrDefault();
            //listing.State = EntityState.Deleted;
           // var listing = db.Listings.Include(e => e.images).FirstOrDefault();
            List<string> names = new List<string>();
            foreach (var lsImage in listing.images.ToList())
            {
                names.Add(lsImage.Name);
                db.ListingImages.Remove(lsImage);
            }
            db.Listings.Remove(listing);
            deleteAllImages(names);
            db.SaveChanges();
            return RedirectToAction("index");

        }
        [HttpPost]
        public ActionResult imageX(int ListingId)
        {
            string desired_path = "~/Images/listings/";
            string user = User.Identity.GetUserId();
            var listQ = from c in db.Listings.Include("images")
                        where c.ListingId == ListingId && c.OwnerId == user
                        select c;
            Listing listing = listQ.FirstOrDefault();
            if (listing == null)
            {
                return Json(new {error = "Error uploading" });
            }
            listing.Updated = DateTime.Now;
            db.Entry(listing).State = EntityState.Modified;
            db.SaveChanges();
            if(listing.images.Count() >= maxImages){
                // you need to delete images first
                return Json(new { error = "Error uploading. Limit reached.",status=403 });
            }
            int x = 1;
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase posted_file = Request.Files[fileName];
                if (posted_file != null && posted_file.ContentLength > 0)
                {
                    //create random file name from guid and upload/save
                    string desired_file_name = ListingId+"_"+System.Guid.NewGuid().ToString("N") + Path.GetExtension(posted_file.FileName);
                    
                    bool file_uploaded = TMLib.TMImage.renameUploadFile(posted_file, desired_path+"/Big", desired_file_name,900);
                    bool med_iplod = TMLib.TMImage.renameUploadFile(posted_file, desired_path + "/Med", desired_file_name, 600);
                    bool thumb_upload = TMLib.TMImage.renameUploadFile(posted_file, desired_path + "/Small", desired_file_name, 200);
                    if(file_uploaded){
                        ListingImage li = new ListingImage();
                        li.ListingId = ListingId;
                        li.Listing = listing;
                        li.Name = desired_file_name;
                        li.DisplayOrder = x++;
                        li.Created = DateTime.Now;
                        db.ListingImages.Add(li);
                        db.SaveChanges();
                    }
                }
                //else there's no valid file uploaded. handle situation here
            }
            //return Json(new { error = "Error uploading. Limit reached.", status = 403 });
             return Json(new { Message = "Uploaded" });
        }
        public ActionResult deleteImage(int listing,int image)
        {
            if (listing <=0 && image <=0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string user = User.Identity.GetUserId();
            var listingImage = from c in db.ListingImages
                               where c.Listing.OwnerId == user && c.ListingId == listing && c.ListingImageId == image
                               select c;
            ListingImage lm = listingImage.FirstOrDefault();
            if(lm == null){
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.ListingImages.Remove(lm);
            db.SaveChanges();
            string[] bases =  {"~/Images/Listings/Big/","~/Images/Listings/Med/","~/Images/Listings/Small/"};
            string file="";
            foreach (string imagesPath in bases)
            {
                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(imagesPath + lm.Name)))
                {
                    System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(imagesPath + lm.Name));
                }
                file = imagesPath + lm.Name;
            }
            
            var json = new {listingId=lm.ListingId,imageId=lm.ListingImageId,file=file };

            return Json(json);
        }
        [AllowAnonymous]
        //public ActionResult search(int? page,string order, string text="",int id=0)
        public ActionResult search(ListingSearchViewModel model)
        {
            string priceOrder = "price";
            string townOrder = "town";
           // model.controller = "listing";
        //    model.action = "search";
            if (ModelState.IsValid)
            {
                //compare max and min price
                if(model.from > model.to){
                    ModelState.AddModelError("", "Max price cannot be less than Min price.");
                }
                Category searchedCategory;
                DateTime now = DateTime.Now;
                var predicate = PredicateBuilder.False<Listing>();
                var listingsQ = from l in db.Listings.Include("Category").Include("images")
                                where l.Status == "live" && l.Expires > now
                                select l;

                //using LinqKit predicate dynamic linq to enable 'OR' logical 'Where' chaining
                if (!String.IsNullOrEmpty(model.text))
                {
                    predicate = predicate.Or(c => c.Title.ToLower().Contains(model.text.ToLower()) || c.Description.ToLower().Contains(model.text.ToLower()));
                }
                if (model.from > 0)
                {
                    predicate = predicate.And(c=>c.Price >= model.from);
                }
                if (model.to > 0)
                {
                    predicate = predicate.And(c => c.Price <= model.to);
                }
                if (!String.IsNullOrEmpty(model.location))
                {
                    predicate = predicate.And(c=>c.Town.ToLower().Contains(model.location.ToLower()));
                }
                //find listigns from this and child categories
                if (model.Category!= null && model.Category.CategoryId > 0)
                {
                    List<Category> categoryTree = categoryHelper.allSubCategories(model.Category.CategoryId);
                    if (categoryTree.Count > 0)
                    {
                        SortedDictionary<string, int> crumbs = new SortedDictionary<string, int>();
                        foreach (var leafNodeCategory in categoryTree)
                        {
                            crumbs.Add(leafNodeCategory.Title, leafNodeCategory.CategoryId);
                        }
                        int[] ids = crumbs.Values.ToArray();
                        //string[] titles = crumbs.Keys.ToArray();
                        predicate = predicate.And(c => ids.Contains(c.CategoryId));
                        searchedCategory = db.Categories.Find(model.cat);
                        //predicate = predicate.Or(c => c.Category.Title.ToLower().Contains(model.text.ToLower()) || c.Category.Controller.ToLower().Contains(model.text.ToLower()) || c.Category.Tags.ToLower().Contains(model.text.ToLower()));
                    }
                }
                //also search for categories like this if id not specified
                if (model.cat <= 0 || model.cat == null)
                {
                    predicate = predicate.Or(c => c.Category.Title.ToLower().Contains(model.text.ToLower()) || c.Category.Controller.ToLower().Contains(model.text.ToLower()) || c.Category.Tags.ToLower().Contains(model.text.ToLower()));
                }
                listingsQ = listingsQ.AsExpandable().Where(predicate);

                //ordering
                listingsQ = listingsQ.OrderByDescending(c => c.Updated);
                if (!String.IsNullOrEmpty(model.order))
                {
                    if (model.order == "price")
                    {
                        listingsQ = listingsQ.OrderBy(c => c.Price);
                        priceOrder = "price_desc";
                    }

                    if (model.order == "price_desc")
                    {
                        listingsQ = listingsQ.OrderByDescending(l => l.Price);
                        priceOrder = "price";
                    }

                    if (model.order == "town")
                    {
                        townOrder = "town_desc";
                        listingsQ = listingsQ.OrderBy(l => l.Town);
                    }
                    if (model.order == "town_desc")
                    {
                        townOrder = "town";
                        listingsQ = listingsQ.OrderByDescending(l => l.Town);
                    }

                }
                else
                {
                    listingsQ = listingsQ.OrderByDescending(c => c.Updated);
                }
                ViewBag.searchViewModel = model;
                ViewBag.priceOrder = priceOrder;
                ViewBag.townOrder = townOrder;
                ViewBag.sortOrder = model.order;
                ViewBag.page = model.page;
                return View(listingsQ.ToPagedList(1, pagesize));
            }
            ViewBag.priceOrder = priceOrder;
            ViewBag.townOrder = townOrder;
            ViewBag.sortOrder = model.order;
            ViewBag.page = 1;
            
            ViewBag.searchViewModel = model;
            List<Listing> type = new List<Listing>();
            return View(type.ToPagedList(1,pagesize));
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void deleteAllImages(List<string> names){
                string[] bases = { "~/Images/Listings/Big/", "~/Images/Listings/Med/", "~/Images/Listings/Small/" };
                foreach (string file in names)
                {
                    foreach (string imagesPath in bases)
                    {
                        if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(imagesPath + file)))
                        {
                            System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(imagesPath + file));
                        }
                    }
                }
        }
    }
}
