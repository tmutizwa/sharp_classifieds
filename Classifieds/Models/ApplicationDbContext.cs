using Classifieds.Areas.Admin.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(): base("DefaultConnection", throwIfV1Schema: false)
        {
            Debug.Write(Database.Connection.ConnectionString);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Classifieds.Areas.Admin.Models.Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().
              HasOptional(e => e.Parent).
              WithMany().
              HasForeignKey(m => m.ParentId);
        }

      //  public System.Data.Entity.DbSet< Motor> Vehicles { get; set; }
        public  DbSet<Motor>Motors{get;set;}
        public  DbSet<Mota> Motas { get; set; }
        public  DbSet<Listing> Listings { get; set; }
        public  DbSet<GeneralListing> GeneralListings { get; set; }
        public  DbSet<ListingImage> ListingImages { get; set; }
        public  DbSet<Cellphone> Cellphones { get; set; }
        public  DbSet<Computer> Computers { get; set; }
        public  DbSet<Job> Jobs { get; set; }
        public  DbSet<Property> Properties { get; set; }
        public  DbSet<Dating> Dates { get; set; }
        public  DbSet<EmailSubscriptions> EmailSubscriptions { get; set; }
        public  DbSet<Deal> Deals { get; set; }
        public  DbSet<Bug> Bugs { get; set; }
        public  DbSet<FeatureRequest> FeatureRequests { get; set; }
        public  DbSet<Message> Messages { get; set; }
        public DbSet<BadListing> BadListings { get; set; }
    }
}