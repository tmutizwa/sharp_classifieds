namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateres : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Listings", "Expires", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Listings", "Expires", c => c.DateTime(nullable: false));
        }
    }
}
