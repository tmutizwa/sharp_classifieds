namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deals : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deals",
                c => new
                    {
                        DealId = c.Int(nullable: false, identity: true),
                        ListingId = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Outreach = c.Int(nullable: false),
                        Condition = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        Starts = c.DateTime(precision: 7, storeType: "datetime2"),
                        Ends = c.DateTime(precision: 7, storeType: "datetime2"),
                        Created = c.DateTime(precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DealId)
                .ForeignKey("dbo.Listings", t => t.ListingId, cascadeDelete: true)
                .Index(t => t.ListingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deals", "ListingId", "dbo.Listings");
            DropIndex("dbo.Deals", new[] { "ListingId" });
            DropTable("dbo.Deals");
        }
    }
}
