using Classifieds.Areas.Admin.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Classifieds.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
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

      //  public System.Data.Entity.DbSet<Classifieds.Models.Motor> Vehicles { get; set; }
        public System.Data.Entity.DbSet<Classifieds.Models.Motor> Motors { get; set; }
        public System.Data.Entity.DbSet<Classifieds.Models.Mota> Motas { get; set; }
        public System.Data.Entity.DbSet<Classifieds.Models.Listing> Listings { get; set; }
        public System.Data.Entity.DbSet<Classifieds.Models.GeneralListing> GeneralListings { get; set; }
        public System.Data.Entity.DbSet<Classifieds.Models.ListingImage> ListingImages { get; set; }
        public System.Data.Entity.DbSet<Classifieds.Models.Cellphone> Cellphones { get; set; }
        public System.Data.Entity.DbSet<Classifieds.Models.Computer> Computers { get; set; }
        public System.Data.Entity.DbSet<Classifieds.Models.Job> Jobs { get; set; }
        public System.Data.Entity.DbSet<Classifieds.Models.Property> Properties { get; set; }
        public System.Data.Entity.DbSet<Classifieds.Models.Dating> Dates { get; set; }
    }
}