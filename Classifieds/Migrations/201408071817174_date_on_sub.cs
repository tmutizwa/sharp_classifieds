namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date_on_sub : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmailSubscriptions", "Started", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.EmailSubscriptions", "Updated", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmailSubscriptions", "Updated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmailSubscriptions", "Started", c => c.DateTime(nullable: false));
        }
    }
}
