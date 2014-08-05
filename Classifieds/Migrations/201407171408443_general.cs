namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class general : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ListingImages", name: "Listing_ListingId1", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.ListingImages", name: "Listing_ListingId", newName: "Listing_ListingId1");
            RenameColumn(table: "dbo.ListingImages", name: "__mig_tmp__0", newName: "Listing_ListingId");
            RenameIndex(table: "dbo.ListingImages", name: "IX_Listing_ListingId1", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.ListingImages", name: "IX_Listing_ListingId", newName: "IX_Listing_ListingId1");
            RenameIndex(table: "dbo.ListingImages", name: "__mig_tmp__0", newName: "IX_Listing_ListingId");
            CreateTable(
                "dbo.GeneralListings",
                c => new
                    {
                        GeneralListingId = c.Int(nullable: false, identity: true),
                        Brand = c.String(maxLength: 50),
                        Condition = c.String(maxLength: 50),
                        ListingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GeneralListingId)
                .ForeignKey("dbo.Listings", t => t.ListingId, cascadeDelete: true)
                .Index(t => t.ListingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GeneralListings", "ListingId", "dbo.Listings");
            DropIndex("dbo.GeneralListings", new[] { "ListingId" });
            DropTable("dbo.GeneralListings");
            RenameIndex(table: "dbo.ListingImages", name: "IX_Listing_ListingId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.ListingImages", name: "IX_Listing_ListingId1", newName: "IX_Listing_ListingId");
            RenameIndex(table: "dbo.ListingImages", name: "__mig_tmp__0", newName: "IX_Listing_ListingId1");
            RenameColumn(table: "dbo.ListingImages", name: "Listing_ListingId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.ListingImages", name: "Listing_ListingId1", newName: "Listing_ListingId");
            RenameColumn(table: "dbo.ListingImages", name: "__mig_tmp__0", newName: "Listing_ListingId1");
        }
    }
}
