namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class motors : DbMigration
    {
        public override void Up()
        {

            DropForeignKey("dbo.Vehicles", "ListingId", "dbo.Listings");
            DropIndex("dbo.Vehicles", new[] { "ListingId" });
            DropTable("dbo.Vehicles");
            CreateTable(
                "dbo.Motors",
                c => new
                    {
                        MotorId = c.Int(nullable: false, identity: true),
                        ListingId = c.Int(nullable: false),
                        Make = c.String(nullable: false),
                        CarModel = c.String(nullable: false),
                        Year = c.String(),
                        Mileage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FuelType = c.String(),
                        Transmission = c.String(),
                        Condition = c.String(),
                        BodyType = c.String(),
                        EngineSize = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MotorId)
                .ForeignKey("dbo.Listings", t => t.ListingId, cascadeDelete: true)
                .Index(t => t.ListingId);
            
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        ListingId = c.Int(nullable: false),
                        Make = c.String(nullable: false),
                        CarModel = c.String(nullable: false),
                        Year = c.String(),
                        Mileage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FuelType = c.String(),
                        Transmission = c.String(),
                        Condition = c.String(),
                        BodyType = c.String(),
                        EngineSize = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.VehicleId);
            
            DropForeignKey("dbo.Motors", "ListingId", "dbo.Listings");
            DropIndex("dbo.Motors", new[] { "ListingId" });
            DropTable("dbo.Motors");
            CreateIndex("dbo.Vehicles", "ListingId");
            AddForeignKey("dbo.Vehicles", "ListingId", "dbo.Listings", "ListingId", cascadeDelete: true);
        }
    }
}
