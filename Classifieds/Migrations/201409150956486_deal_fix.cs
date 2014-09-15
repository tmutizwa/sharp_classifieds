namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deal_fix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deals", "DealId", "dbo.Listings");
            //DropColumn("dbo.Deals", "ListingId");
            //RenameColumn(table: "dbo.Deals", name: "DealId", newName: "ListingId");
            //RenameIndex(table: "dbo.Deals", name: "IX_DealId", newName: "IX_ListingId");
            DropPrimaryKey("dbo.Deals");
            AlterColumn("dbo.Deals", "DealId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Deals", "DealId");
            //AddForeignKey("dbo.Deals", "ListingId", "dbo.Listings", "ListingId", cascadeDelete: true);
           // DropColumn("dbo.Listings", "DealId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Listings", "DealId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Deals", "ListingId", "dbo.Listings");
            DropPrimaryKey("dbo.Deals");
            AlterColumn("dbo.Deals", "DealId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Deals", "DealId");
            RenameIndex(table: "dbo.Deals", name: "IX_ListingId", newName: "IX_DealId");
            RenameColumn(table: "dbo.Deals", name: "ListingId", newName: "DealId");
            AddColumn("dbo.Deals", "ListingId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Deals", "DealId", "dbo.Listings", "ListingId");
        }
    }
}
