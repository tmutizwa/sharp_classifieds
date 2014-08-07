namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subsUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailSubscriptions", "Updated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmailSubscriptions", "Updated");
        }
    }
}
