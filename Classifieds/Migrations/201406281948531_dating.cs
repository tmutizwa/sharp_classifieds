namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dating : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Listings", "Expires", c => c.DateTime(nullable: true, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
        }
    }
}
