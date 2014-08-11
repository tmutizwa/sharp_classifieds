namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ClassifiedsPhone", c => c.String());
            AddColumn("dbo.AspNetUsers", "Pic", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Pic");
            DropColumn("dbo.AspNetUsers", "ClassifiedsPhone");
        }
    }
}
