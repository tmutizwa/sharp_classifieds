namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catgry : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "SearchModel", c => c.String());
            AlterColumn("dbo.Categories", "ViewModel", c => c.String());
            DropColumn("dbo.Categories", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Categories", "ViewModel", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "SearchModel", c => c.String(nullable: false));
        }
    }
}
