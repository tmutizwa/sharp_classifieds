namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mota : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Vehicles", "ListingId", "dbo.Listings");
           // DropIndex("dbo.Vehicles", new[] { "ListingId" });
           // DropColumn("dbo.Vehicles", "ListingId");
           // RenameColumn(table: "dbo.Vehicles", name: "Listing_ListingId", newName: "ListingId");
            //DropPrimaryKey("dbo.Vehicles","ListingId");
          //  AddColumn("dbo.Vehicles", "VehicleId", c => c.Int(nullable: false, identity: true));
           // AlterColumn("dbo.Vehicles", "ListingId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Vehicles", "ListingId", c => c.Int(nullable: false));
            //AddPrimaryKey("dbo.Vehicles", "VehicleId");
           // CreateIndex("dbo.Vehicles", "ListingId");
          //  AddForeignKey("dbo.Vehicles", "ListingId", "dbo.Listings", "ListingId", cascadeDelete: true);
        }
        
        public override void Down()
        {
           // DropForeignKey("dbo.Vehicles", "ListingId", "dbo.Listings");
            DropIndex("dbo.Vehicles", new[] { "ListingId" });
            DropPrimaryKey("dbo.Vehicles");
            AlterColumn("dbo.Vehicles", "ListingId", c => c.Int());
            AlterColumn("dbo.Vehicles", "ListingId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Vehicles", "VehicleId");
            AddPrimaryKey("dbo.Vehicles", "ListingId");
            RenameColumn(table: "dbo.Vehicles", name: "ListingId", newName: "Listing_ListingId");
            AddColumn("dbo.Vehicles", "ListingId", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Vehicles", "Listing_ListingId");
            AddForeignKey("dbo.Vehicles", "Listing_ListingId", "dbo.Listings", "ListingId");
        }
    }
}
