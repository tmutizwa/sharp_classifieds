namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catsmodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "SearchModel", c => c.String(nullable: false));
            AddColumn("dbo.Categories", "ViewModel", c => c.String(nullable: false));
            DropColumn("dbo.Categories", "Model");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Model", c => c.String(nullable: false));
            DropColumn("dbo.Categories", "ViewModel");
            DropColumn("dbo.Categories", "SearchModel");
        }
    }
}
