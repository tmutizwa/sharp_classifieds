namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class images : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListingImages",
                c => new
                    {
                        ListingImageId = c.Int(nullable: false, identity: true),
                        ListingId = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        SizeGroup = c.String(),
                        Status = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.ListingImageId)
                .ForeignKey("dbo.Listings", t => t.ListingId, cascadeDelete: true)
                .Index(t => t.ListingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListingImages", "ListingId", "dbo.Listings");
            DropIndex("dbo.ListingImages", new[] { "ListingId" });
            DropTable("dbo.ListingImages");
        }
    }
}
