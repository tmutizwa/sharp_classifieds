namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cehkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vehicles", "listingId", "dbo.Listings");
            DropIndex("dbo.Vehicles", new[] { "listingId" });
           // DropPrimaryKey("dbo.Vehicles");
           // AddColumn("dbo.Vehicles", "Listing_ListingId", c => c.Int());
            AlterColumn("dbo.Vehicles", "ListingId", c => c.Int(nullable: false, identity: true));
           // AddPrimaryKey("dbo.Vehicles", "ListingId");
            CreateIndex("dbo.Vehicles", "ListingId");
            AddForeignKey("dbo.Vehicles", "ListingId", "dbo.Listings", "ListingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "ListingId", "dbo.Listings");
            DropIndex("dbo.Vehicles", new[] { "ListingId" });
            DropPrimaryKey("dbo.Vehicles","ListingId");
            AlterColumn("dbo.Vehicles", "ListingId", c => c.Int(nullable: false));
          //  DropColumn("dbo.Vehicles", "Listing_ListingId");
            AddPrimaryKey("dbo.Vehicles", "listingId");
            CreateIndex("dbo.Vehicles", "listingId");
            AddForeignKey("dbo.Vehicles", "listingId", "dbo.Listings", "ListingId");
        }
    }
}
