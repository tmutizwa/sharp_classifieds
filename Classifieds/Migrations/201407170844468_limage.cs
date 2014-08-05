namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class limage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ListingImages", "ListingId", "dbo.Listings");
            DropIndex("dbo.ListingImages", new[] { "ListingId" });
            AddColumn("dbo.ListingImages", "Listing_ListingId", c => c.Int());
            AddColumn("dbo.ListingImages", "Listing_ListingId1", c => c.Int());
            AddColumn("dbo.Listings", "ListingImage_ListingImageId", c => c.Int());
            CreateIndex("dbo.ListingImages", "Listing_ListingId");
            CreateIndex("dbo.ListingImages", "Listing_ListingId1");
            CreateIndex("dbo.Listings", "ListingImage_ListingImageId");
            AddForeignKey("dbo.Listings", "ListingImage_ListingImageId", "dbo.ListingImages", "ListingImageId");
            AddForeignKey("dbo.ListingImages", "Listing_ListingId1", "dbo.Listings", "ListingId");
            AddForeignKey("dbo.ListingImages", "Listing_ListingId", "dbo.Listings", "ListingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListingImages", "Listing_ListingId", "dbo.Listings");
            DropForeignKey("dbo.ListingImages", "Listing_ListingId1", "dbo.Listings");
            DropForeignKey("dbo.Listings", "ListingImage_ListingImageId", "dbo.ListingImages");
            DropIndex("dbo.Listings", new[] { "ListingImage_ListingImageId" });
            DropIndex("dbo.ListingImages", new[] { "Listing_ListingId1" });
            DropIndex("dbo.ListingImages", new[] { "Listing_ListingId" });
            DropColumn("dbo.Listings", "ListingImage_ListingImageId");
            DropColumn("dbo.ListingImages", "Listing_ListingId1");
            DropColumn("dbo.ListingImages", "Listing_ListingId");
            CreateIndex("dbo.ListingImages", "ListingId");
            AddForeignKey("dbo.ListingImages", "ListingId", "dbo.Listings", "ListingId", cascadeDelete: true);
        }
    }
}
