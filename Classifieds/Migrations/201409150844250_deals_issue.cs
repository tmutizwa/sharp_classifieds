namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deals_issue : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Listings", "BulkUploaded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Listings", "BulkUploaded", c => c.Int(nullable: false));
        }
    }
}
