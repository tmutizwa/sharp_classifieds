namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class images3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ListingImages", "DisplayOrder", c => c.Int(nullable: false));
            AddColumn("dbo.ListingImages", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ListingImages", "Created");
            DropColumn("dbo.ListingImages", "DisplayOrder");
        }
    }
}
