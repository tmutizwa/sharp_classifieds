namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Status");
        }
    }
}
