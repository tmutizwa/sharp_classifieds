namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deal_listing_rshp : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Deals", "ListingId", "dbo.Listings");
            //DropIndex("dbo.Deals", new[] { "ListingId" });
            //DropColumn("dbo.Deals", "DealId");
            //RenameColumn(table: "dbo.Deals", name: "ListingId", newName: "DealId");
            //DropPrimaryKey("dbo.Deals");
            //AddColumn("dbo.Listings", "DealId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Deals", "DealId", c => c.Int(nullable: false));
            //AddPrimaryKey("dbo.Deals", "DealId");
            //CreateIndex("dbo.Deals", "DealId");
            //AddForeignKey("dbo.Deals", "DealId", "dbo.Listings", "ListingId");
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Deals", "DealId", "dbo.Listings");
            //DropIndex("dbo.Deals", new[] { "DealId" });
            //DropPrimaryKey("dbo.Deals");
            //AlterColumn("dbo.Deals", "DealId", c => c.Int(nullable: false, identity: true));
            //DropColumn("dbo.Listings", "DealId");
            //AddPrimaryKey("dbo.Deals", "DealId");
            //RenameColumn(table: "dbo.Deals", name: "DealId", newName: "ListingId");
            //AddColumn("dbo.Deals", "DealId", c => c.Int(nullable: false, identity: true));
            //CreateIndex("dbo.Deals", "ListingId");
            //AddForeignKey("dbo.Deals", "ListingId", "dbo.Listings", "ListingId", cascadeDelete: true);
        }
    }
}
