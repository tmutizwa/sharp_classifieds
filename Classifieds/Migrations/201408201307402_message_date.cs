namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class message_date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Created", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Messages", "Updated", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Updated");
            DropColumn("dbo.Messages", "Created");
        }
    }
}
