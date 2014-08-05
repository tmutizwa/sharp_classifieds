using Classifieds.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Classifieds.Models
{
    public class ClassifiedsCreateListingModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            // Get the submitted type - should be AlistingSearchViewModel
            var type = bindingContext.ModelType;
            var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            //get the model type submitted by user, we use the 'mtype' field to determine which model to use
            int cat = 0;
            try
            {
                cat = (int)bindingContext.ValueProvider.GetValue("CategoryId").ConvertTo(typeof(int));
            }
            catch (Exception e)
            {
                throw new Exception("Invalid request.");
            }
            ApplicationDbContext db = new ApplicationDbContext();
            var catQ = from c in db.Categories
                       where c.CategoryId == cat && c.Status == "live"
                       select c;
            if (catQ == null)
            {
                throw new Exception("Invalid request.");
            }
            Category category = catQ.FirstOrDefault();
            string model = "";
            try
            {
                model = category.ViewModel;
            }
            catch (Exception)
            {
                throw new Exception("ViewModel not set up.");
            }
            //get the type user trying to declare/use from the system
            modelType = Type.GetType("Classifieds.Models.CreateViewModels.Create" + model + ", " + assembly);
            //now create the concrete type if found
            if (modelType != null)
            {
                object[] args = new object[] { category.CategoryId};
                var instance = Activator.CreateInstance(modelType,args);
                bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => instance, modelType);
                if(instance == null)
                    throw new Exception("CreateViewModel not set up.");
                return instance;
            }
            else throw new Exception("Invalid model type. Check create view model file exists.");

        }
    }
}