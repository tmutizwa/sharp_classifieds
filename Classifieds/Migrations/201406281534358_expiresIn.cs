namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expiresIn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ExpiresIn", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "ExpiresIn");
        }
    }
}
