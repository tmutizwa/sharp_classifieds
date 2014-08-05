namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class images1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Listings", "ListingImage_ListingImageId", "dbo.ListingImages");
            DropForeignKey("dbo.ListingImages", "Listing_ListingId1", "dbo.Listings");
            DropForeignKey("dbo.ListingImages", "Listing_ListingId", "dbo.Listings");
            DropIndex("dbo.Listings", new[] { "ListingImage_ListingImageId" });
            DropIndex("dbo.ListingImages", new[] { "Listing_ListingId" });
            DropIndex("dbo.ListingImages", new[] { "Listing_ListingId1" });
            DropColumn("dbo.ListingImages", "ListingId");
            DropColumn("dbo.ListingImages", "Listing_ListingId");
            RenameColumn(table: "dbo.ListingImages", name: "Listing_ListingId1", newName: "ListingId");
           // RenameColumn(table: "dbo.ListingImages", name: "Listing_ListingId", newName: "ListingId");
            AlterColumn("dbo.ListingImages", "ListingId", c => c.Int(nullable: false));
            //AlterColumn("dbo.ListingImages", "ListingId", c => c.Int(nullable: false));
            CreateIndex("dbo.ListingImages", "ListingId");
            AddForeignKey("dbo.ListingImages", "ListingId", "dbo.Listings", "ListingId", cascadeDelete: true);
            DropColumn("dbo.Listings", "ListingImageId");
            DropColumn("dbo.Listings", "ListingImage_ListingImageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Listings", "ListingImage_ListingImageId", c => c.Int());
            AddColumn("dbo.Listings", "ListingImageId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ListingImages", "ListingId", "dbo.Listings");
            DropIndex("dbo.ListingImages", new[] { "ListingId" });
            AlterColumn("dbo.ListingImages", "ListingId", c => c.Int());
            AlterColumn("dbo.ListingImages", "ListingId", c => c.Int());
            RenameColumn(table: "dbo.ListingImages", name: "ListingId", newName: "Listing_ListingId");
            RenameColumn(table: "dbo.ListingImages", name: "ListingId", newName: "Listing_ListingId1");
            AddColumn("dbo.ListingImages", "ListingId", c => c.Int(nullable: false));
            AddColumn("dbo.ListingImages", "ListingId", c => c.Int(nullable: false));
            CreateIndex("dbo.ListingImages", "Listing_ListingId1");
            CreateIndex("dbo.ListingImages", "Listing_ListingId");
            CreateIndex("dbo.Listings", "ListingImage_ListingImageId");
            AddForeignKey("dbo.ListingImages", "Listing_ListingId", "dbo.Listings", "ListingId");
            AddForeignKey("dbo.ListingImages", "Listing_ListingId1", "dbo.Listings", "ListingId");
            AddForeignKey("dbo.Listings", "ListingImage_ListingImageId", "dbo.ListingImages", "ListingImageId");
        }
    }
}
