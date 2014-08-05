using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Classifieds.Models
{
    public class ClassifiedsEditListingModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            // Get the submitted type - should be AlistingSearchViewModel
            var type = bindingContext.ModelType;
            var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            //get the model type submitted by user, we use the 'mtype' field to determine which model to use
            int id = 0;
            try
            {
                id = (int)bindingContext.ValueProvider.GetValue("ListingId").ConvertTo(typeof(int));
            }
            catch (Exception e)
            {
                throw new Exception("Invalid request.");
            }
            ApplicationDbContext db = new ApplicationDbContext();
            var lQ = from c in db.Listings.Include("Category")
                       where c.ListingId == id
                       select c;
            if (lQ == null)
            {
                throw new Exception("Invalid request.");
            }
            var listing = lQ.FirstOrDefault();
            string model = "";
            try
            {
                model = listing.Category.ViewModel;
            }
            catch (Exception)
            {
                throw new Exception("ViewModel not set up for category.");
            }
            //get the type user trying to declare/use from the system
            modelType = Type.GetType("Classifieds.Models.EditViewModels.Edit" + model + ", " + assembly);
            //now create the concrete type if found
            if (modelType != null)
            {
                object[] args = new object[] {  };
                var instance = Activator.CreateInstance(modelType, args);
                bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => instance, modelType);
                return instance;
            }
            else throw new Exception("Invalid model type. Check create view model file exists.");

        }
    }
}