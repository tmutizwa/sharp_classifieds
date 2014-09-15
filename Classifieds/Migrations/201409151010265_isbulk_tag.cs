namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isbulk_tag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deals", "BulkUploaded", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deals", "BulkUploaded");
        }
    }
}
