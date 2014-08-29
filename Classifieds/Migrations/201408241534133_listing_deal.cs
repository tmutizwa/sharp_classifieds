namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listing_deal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deals", "ListingId", "dbo.Listings");
            DropPrimaryKey("dbo.Deals");
            AddColumn("dbo.Listings", "DealId", c => c.Int(nullable: false));
            AlterColumn("dbo.Deals", "DealId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Deals", "ListingId");
            AddForeignKey("dbo.Deals", "ListingId", "dbo.Listings", "ListingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deals", "ListingId", "dbo.Listings");
            DropPrimaryKey("dbo.Deals");
            AlterColumn("dbo.Deals", "DealId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Listings", "DealId");
            AddPrimaryKey("dbo.Deals", "DealId");
            AddForeignKey("dbo.Deals", "ListingId", "dbo.Listings", "ListingId", cascadeDelete: true);
        }
    }
}
