namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cellphone2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cellphones", "ListingId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cellphones", "ListingId");
            AddForeignKey("dbo.Cellphones", "ListingId", "dbo.Listings", "ListingId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cellphones", "ListingId", "dbo.Listings");
            DropIndex("dbo.Cellphones", new[] { "ListingId" });
            DropColumn("dbo.Cellphones", "ListingId");
        }
    }
}
