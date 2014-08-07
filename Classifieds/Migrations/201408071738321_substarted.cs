namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class substarted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailSubscriptions", "Started", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmailSubscriptions", "Started");
        }
    }
}
