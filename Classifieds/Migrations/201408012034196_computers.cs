namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class computers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Computers",
                c => new
                    {
                        ComputerId = c.Int(nullable: false, identity: true),
                        ListingId = c.Int(nullable: false),
                        OS = c.String(),
                        Brand = c.String(),
                        HddSize = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ram = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ScreenSize = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Processor = c.String(),
                    })
                .PrimaryKey(t => t.ComputerId)
                .ForeignKey("dbo.Listings", t => t.ListingId, cascadeDelete: true)
                .Index(t => t.ListingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Computers", "ListingId", "dbo.Listings");
            DropIndex("dbo.Computers", new[] { "ListingId" });
            DropTable("dbo.Computers");
        }
    }
}
