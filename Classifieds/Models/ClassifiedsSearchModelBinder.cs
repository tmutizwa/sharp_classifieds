using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Classifieds.Models.SearchViewModels;
using Classifieds.Models;
using Classifieds.Models.ViewModels;
using Classifieds.Areas.Admin.Models;
using System.Net;


namespace Classifieds.Models
{
    public class ClassifiedsSearchModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext,Type modelType)
        {
            // Get the submitted type - should be AlistingSearchViewModel
            var type = bindingContext.ModelType;
            var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            //get the model type submitted by user, we use the 'catgry' field to determine which model to use
            int cat = 0;
            try
            {
                cat =  (int)bindingContext.ValueProvider.GetValue("cat").ConvertTo(typeof(int));
            }catch(Exception e){
                throw new Exception("Invalid request.", e);
            }

            ApplicationDbContext db = new ApplicationDbContext();
            var catQ = from c in db.Categories
                       where c.CategoryId == cat && c.Status == "live"
                       select c;
            if (catQ == null)
            {
                throw new Exception("Category unavailable.");
            }
            Category category = catQ.FirstOrDefault();
            string model;
            if(category == null)
                model = "ListingSearchViewModel";
            try
            {
                model = category.SearchModel;
            }
            catch (Exception)
            {
                model = "ListingSearchViewModel";
            }
            //get the type user trying to declare/use from the system
            modelType = Type.GetType("Classifieds.Models.SearchViewModels."+model+ ", " + assembly);
            //now create the concrete type if found
            if (modelType != null)
            {
                // Update the binding context's meta data
                object[] args = new object[] { category};
                var instance = Activator.CreateInstance(modelType,args);
                bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => instance, modelType);
                return instance;
            }
            else throw new Exception("Invalid model type specified.");
            
        }
    }
}