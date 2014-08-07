namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subs_date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailSubscriptions", "Started", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.EmailSubscriptions", "Updated", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmailSubscriptions", "Updated");
            DropColumn("dbo.EmailSubscriptions", "Started");
        }
    }
}
