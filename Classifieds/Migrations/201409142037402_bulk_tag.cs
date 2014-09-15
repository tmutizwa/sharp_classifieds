namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bulk_tag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Listings", "BulkUploaded", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Listings", "BulkUploaded");
        }
    }
}
