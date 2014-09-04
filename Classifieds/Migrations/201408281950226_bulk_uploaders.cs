namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bulk_uploaders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BulkUploaderId", c => c.String());
            AddColumn("dbo.AspNetUsers", "BulkUploaderName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BulkUploaderName");
            DropColumn("dbo.AspNetUsers", "BulkUploaderId");
        }
    }
}
