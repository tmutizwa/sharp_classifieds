namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carfixer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Motas",
                c => new
                    {
                        MotaId = c.Int(nullable: false, identity: true),
                        ListingId = c.Int(nullable: false),
                        Make = c.String(nullable: false, maxLength: 50),
                        CarModel = c.String(nullable: false, maxLength: 50),
                        Year = c.String(maxLength: 4),
                        Mileage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FuelType = c.String(maxLength: 50),
                        Transmission = c.String(maxLength: 50),
                        Condition = c.String(maxLength: 50),
                        BodyType = c.String(maxLength: 50),
                        EngineSize = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MotaId)
                .ForeignKey("dbo.Listings", t => t.ListingId, cascadeDelete: true)
                .Index(t => t.ListingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Motas", "ListingId", "dbo.Listings");
            DropIndex("dbo.Motas", new[] { "ListingId" });
            DropTable("dbo.Motas");
        }
    }
}
