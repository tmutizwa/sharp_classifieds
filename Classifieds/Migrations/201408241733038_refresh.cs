namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refresh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deals", "ListingId", "dbo.Listings");
            DropPrimaryKey("dbo.Deals");
            AlterColumn("dbo.Deals", "DealId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Deals", "DealId");
            AddForeignKey("dbo.Deals", "ListingId", "dbo.Listings", "ListingId", cascadeDelete: true);
            DropColumn("dbo.Listings", "DealId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Listings", "DealId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Deals", "ListingId", "dbo.Listings");
            DropPrimaryKey("dbo.Deals");
            AlterColumn("dbo.Deals", "DealId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Deals", "ListingId");
            AddForeignKey("dbo.Deals", "ListingId", "dbo.Listings", "ListingId");
        }
    }
}
