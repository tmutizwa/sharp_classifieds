namespace Classifieds.Migrations
{
    using Classifieds.Areas.Admin.Models;
    using Classifieds.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Classifieds.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Classifieds.Models.ApplicationDbContext context)
        {
            // var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            //add roles
            var roles = new List<IdentityRole>(){
                 new IdentityRole{ Name = "Administrator"},
                 new IdentityRole{ Name = "SuperAdmin" },
                 new IdentityRole{ Name = "Registered"},
                 new IdentityRole{ Name = "Store"},
                };
            // roles.ForEach(r=>context.Roles.AddOrUpdate(p=>p.Name,r));
            foreach (var role in roles)
            {
                context.Roles.AddOrUpdate(r => r.Name, role);
            }
            context.SaveChanges();

            //add users
            var users = new List<ApplicationUser>{
                new ApplicationUser{UserName = "terry@one.com",Email="terry@one.com",FullName="Terrance Mutizwa",Suspended=false},
                new ApplicationUser{UserName = "allen@email.com",Email="allen@email.com",FullName="Allen Chinyangare",Suspended=false},
                new ApplicationUser{UserName = "kudzie@email.com",Email="kudzie@email.com",FullName="Kudzai Sadomba",Suspended=true},
                new ApplicationUser{UserName = "gillmore@email.com",Email="gillmore@email.com",FullName="Gillmour Tunhira",Suspended=false}
            };
            foreach (var user in users)
            {
                userManager.Create(user, "TerryPassword123!");
                if (user.UserName == "terry@one.com")
                    user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = context.Roles.Single(r => r.Name == "SuperAdmin").Id });
                else
                    user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = context.Roles.Single(r => r.Name == "Administrator").Id });
            }
            context.SaveChanges();
            //add categories
            //var categories = new List<Category> { 
            //  //new Category(){Status="live", Title="Automotive/Vehicles",SearchModel="AutosSearchViewModel",ViewModel="AutosViewModel"},
            //  //new Category(){Status="live", Title="Jobs",SearchModel="ListingSearchViewModel",ViewModel="AutosViewModel"},
            //  //new Category(){Status="live", Title="Electronics & Computers",SearchModel="ListingSearchViewModel",ViewModel="AutosViewModel"},
            //  //new Category(){Status="live", Title="Fashion, Beauty & Health",SearchModel="ListingSearchViewModel",ViewModel="AutosViewModel"},
            //  //new Category(){Status="live", Title="Property Sales & Rentals",SearchModel="ListingSearchViewModel",ViewModel="AutosViewModel"},
            //  //new Category(){Status="live", Title="Services",SearchModel="ListingSearchViewModel",ViewModel="AutosViewModel"},
            //  //new Category(){Status="live", Title="Farming & Industrial",SearchModel="ListingSearchViewModel",ViewModel="AutosViewModel"},
            //  //new Category(){Status="live", Title="Home, Office & Tools",SearchModel="ListingSearchViewModel",ViewModel="AutosViewModel"},
            //  //new Category(){Status="live", Title="Sports & Outdoor",SearchModel="ListingSearchViewModel",ViewModel="AutosViewModel"},
            //  //new Category(){Status="live", Title="For Sale",SearchModel="ListingSearchViewModel",ViewModel="AutosViewModel"},
            //  //new Category(){Status="live", Title="Music,Books & Entertainment",SearchModel="ListingSearchViewModel",ViewModel="AutosViewModel"},
            //  //new Category(){Status="live", Title="Dating",SearchModel="ListingSearchViewModel",ViewModel="AutosViewModel"},
            //  //new Category(){Status="live", Title="Wanted",SearchModel="ListingSearchViewModel",ViewModel="AutosViewModel"},

            //};
            //foreach (var category in categories)
            //{
            //    context.Categories.AddOrUpdate(c => c.Title, category);
            //}
            //context.SaveChanges();

            //foreach (var cat in categories)
            //{
            //    switch (cat.Title)
            //    {
            //        case "Automotive/Vehicles":
            //            context.Categories.AddOrUpdate(c => c.Title, new Category { Status = "live", SearchModel = "AutosSearchViewModel", ViewModel = "AutosViewModel", ExpiresIn = 4, Title = "Trucks", ParentId = context.Categories.Single(t => t.Title == "Automotive/Vehicles").CategoryId });
            //            break;
            //        case "Jobs":
            //            context.Categories.AddOrUpdate(c => c.Title, new Category { Status = "live", SearchModel = "Jobs", ViewModel = "AutosViewModel", ExpiresIn = 3, Title = "ICT",  ParentId = context.Categories.Single(t => t.Title == "Jobs").CategoryId });
            //            break;
            //        case "Fashion, Beauty & Health":
            //            context.Categories.AddOrUpdate(g => g.Title, new Category { Status = "live", SearchModel = "fashion", ViewModel = "AutosViewModel", ExpiresIn = 3, Title = "Men", ParentId = context.Categories.Single(t => t.Title == "Fashion, Beauty & Health").CategoryId });
            //            break;
            //    }
            //}
            //context.SaveChanges();
        }
    }
}
