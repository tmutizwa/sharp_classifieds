namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dating1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Datings",
                c => new
                    {
                        DatingId = c.Int(nullable: false, identity: true),
                        ListingId = c.Int(nullable: false),
                        Type = c.String(),
                        Age = c.Int(nullable: false),
                        Sex = c.String(),
                        Interests = c.String(),
                        Religion = c.String(),
                        Occupation = c.String(),
                        Nationality = c.String(),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ethnicity = c.String(),
                        Smoke = c.String(),
                        Drink = c.String(),
                    })
                .PrimaryKey(t => t.DatingId)
                .ForeignKey("dbo.Listings", t => t.ListingId, cascadeDelete: true)
                .Index(t => t.ListingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Datings", "ListingId", "dbo.Listings");
            DropIndex("dbo.Datings", new[] { "ListingId" });
            DropTable("dbo.Datings");
        }
    }
}
