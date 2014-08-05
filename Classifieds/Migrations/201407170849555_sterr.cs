namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sterr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Motors", "Steering", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Motors", "Steering");
        }
    }
}
