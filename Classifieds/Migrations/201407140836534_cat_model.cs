namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cat_model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Model", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Model");
        }
    }
}
