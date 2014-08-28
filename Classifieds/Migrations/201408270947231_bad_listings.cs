namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bad_listings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BadListings",
                c => new
                    {
                        BadListingId = c.Int(nullable: false, identity: true),
                        ListingId = c.Int(nullable: false),
                        Reason = c.String(),
                        Created = c.DateTime(precision: 7, storeType: "datetime2"),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.BadListingId)
                .ForeignKey("dbo.Listings", t => t.ListingId, cascadeDelete: true)
                .Index(t => t.ListingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BadListings", "ListingId", "dbo.Listings");
            DropIndex("dbo.BadListings", new[] { "ListingId" });
            DropTable("dbo.BadListings");
        }
    }
}
