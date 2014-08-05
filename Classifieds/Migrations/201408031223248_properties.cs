namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class properties : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        PropertyId = c.Int(nullable: false, identity: true),
                        ListingId = c.Int(nullable: false),
                        Suburb = c.String(),
                        Bedrooms = c.Int(nullable: false),
                        Bathrooms = c.Int(nullable: false),
                        Toilets = c.Int(nullable: false),
                        Garages = c.Int(nullable: false),
                        Boreholes = c.Int(nullable: false),
                        Power = c.String(),
                        LandArea = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuildingArea = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PropertyId)
                .ForeignKey("dbo.Listings", t => t.ListingId, cascadeDelete: true)
                .Index(t => t.ListingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Properties", "ListingId", "dbo.Listings");
            DropIndex("dbo.Properties", new[] { "ListingId" });
            DropTable("dbo.Properties");
        }
    }
}
