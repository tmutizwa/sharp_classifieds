namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_deal_rshp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deals", "ListingId", "dbo.Listings");
            DropPrimaryKey("dbo.Deals");
            AddPrimaryKey("dbo.Deals", "ListingId");
            AddForeignKey("dbo.Deals", "ListingId", "dbo.Listings", "ListingId");
            DropColumn("dbo.Deals", "DealId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deals", "DealId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Deals", "ListingId", "dbo.Listings");
            DropPrimaryKey("dbo.Deals");
            AddPrimaryKey("dbo.Deals", "DealId");
            AddForeignKey("dbo.Deals", "ListingId", "dbo.Listings", "ListingId", cascadeDelete: true);
        }
    }
}
