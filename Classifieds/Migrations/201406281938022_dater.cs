namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dater : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Listings", "Created", c => c.DateTime(nullable: true, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Listings", "Updated", c => c.DateTime(nullable: true, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Listings", "Expires", c => c.DateTime(nullable: true, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Listings", "Expires", c => c.DateTime(nullable: true, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Listings", "Updated", c => c.DateTime(nullable: true, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Listings", "Created", c => c.DateTime(nullable: true, precision: 7, storeType: "datetime2"));
        }
    }
}
