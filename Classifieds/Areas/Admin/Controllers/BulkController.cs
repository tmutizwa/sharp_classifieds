using Classifieds.Areas.Admin.Models.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Classifieds.Areas.Admin.Models;
using System.Text;
using Classifieds.Models;
using System.Collections.Generic;
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
            ApplicationDbContext db = new ApplicationDbContext();
            if (ModelState.IsValid)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<BulkPrintClassifieds>));
                DataContractJsonSerializer mapperSer = new DataContractJsonSerializer(typeof(List<BulkPrintClassifiedsMapper>));
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(model.ContentJson));
                MemoryStream mapStream = new MemoryStream(Encoding.UTF8.GetBytes(model.MapperJson));
                var ads = (List<BulkPrintClassifieds>)ser.ReadObject(stream);
                var mapperList = (List<BulkPrintClassifiedsMapper>)mapperSer.ReadObject(mapStream);
                //add all mappings to a dictionary that we will search from later
                Dictionary<string, int> mapper = new Dictionary<string, int>();
                foreach (var mapping in mapperList)
                {
                    if (!mapper.ContainsKey(mapping.print_category))
                    {
                        mapper.Add(mapping.print_category, mapping.web_category_id);
                    }
                    
                }
                
                foreach (var ad in ads)
                {
                    if(mapper.ContainsKey(ad.title) && mapper[ad.title] > 0){
                        var ls = new Listing();
                        ls.Created = DateTime.Now;
                        ls.CategoryId = mapper[ad.title];
                        ls.Description = ad.Details;
                        ls.Expires = DateTime.Now.AddMonths(1);
                        ls.OwnerId = "e477dd37-86c5-448b-9a62-ef8af46e5c4b";
                        ls.Status = "live";
                        ls.Title = ad.title;
                        ls.Updated = DateTime.Now;
                        ls.BulkUploaded = 1;
                        db.Listings.Add(ls);
                    }
                    
                }
                db.SaveChanges();
                ViewBag.Message = "Successfully uploaded print listings";
            }
            return View(model);
        }
    }
}