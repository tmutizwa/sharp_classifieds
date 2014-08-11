namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ClassifiedsPhone", c => c.String());
            DropColumn("dbo.AspNetUsers", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
            DropColumn("dbo.AspNetUsers", "ClassifiedsPhone");
        }
    }
}
