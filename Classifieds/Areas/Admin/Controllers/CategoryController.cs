using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Classifieds.Areas.Admin.Models;
using Classifieds.Models;
using PagedList;

namespace Classifieds.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        int pagesize = 15;

        // GET: Admin/Category
        public ActionResult Index(string ctitle,string r,string parent,string searchmodel,string viewmodel,int id=0,int page = 1)
        {
            if(!String.IsNullOrEmpty(r))
                return RedirectToLocal(r);
            var categories = from c in db.Categories.Include("Parent")
                             select c;
            if (!String.IsNullOrEmpty(ctitle))
            {
                categories = categories.Where(c => c.Title.ToUpper().Contains(ctitle.ToUpper()));
            }
            if(!String.IsNullOrEmpty(parent)){
                categories = categories.Where(c=>c.Parent.Title.ToUpper().Contains(parent.ToUpper()));
            }
            if (!String.IsNullOrEmpty(viewmodel))
            {
                categories = categories.Where(c => c.ViewModel.ToUpper().Contains(viewmodel.ToUpper()));
            }
            if (!String.IsNullOrEmpty(searchmodel))
            {
                categories = categories.Where(c => c.SearchModel.ToUpper().Contains(searchmodel.ToUpper()));
            }
            if(id > 0){
                categories = categories.Where(c=>c.CategoryId == id);
            }
            categories = categories.OrderBy(c => c.Order);
            ViewBag.returnUrl = Request.RawUrl;
            ViewBag.parent = parent;
            ViewBag.ctitle = ctitle;
            ViewBag.viewModel = viewmodel;
            ViewBag.searchmodel = searchmodel;
            return View(categories.ToPagedList(page, pagesize));
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        public ActionResult Create(string r)
        {
            ViewBag.returnUrl = r;
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order,SearchModel,ViewModel,Status,Name,Title,ExpiresIn,Description,ParentId")] Category category,string r)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.Status = "live";
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToLocal(r);
                }
            }
            catch (DataException e)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator."+e.Message);
                //catch(DbEntityValidationException dbEx)
                //{
                //    foreach (var validationErrors in dbEx.EntityValidationErrors)
                //    {
                //        foreach (var validationError in validationErrors.ValidationErrors)
                //        {
                //            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                //            ModelState.AddModelError("", validationError.ErrorMessage);
                //        }
                //    }
                //    //we need to reset the category since it's gone now from the form submission
                //    model.Category = db.Categories.Find(model.CategoryId);
                //}

            }

            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id,string r)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.returnUrl = r;
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order,SearchModel,ViewModel,Status,CategoryId,ExpiresIn,Controller,Name,Title,Description,ParentId")] Category category,string r)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToLocal(r);
                    //return RedirectToAction("Index");
                    //return Redirect(r);
                }
            }
            catch (DataException cc)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator. Error details: "+cc.Message);
            }
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id,string r)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.returnUrl = r;
            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,string r)
        {
                Category category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
               // return RedirectToAction("Index");
                return RedirectToLocal(r);
            
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("index", "category");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
