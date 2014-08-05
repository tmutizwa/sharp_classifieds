namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class computers3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Computers", "Condition", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Computers", "Condition");
        }
    }
}
