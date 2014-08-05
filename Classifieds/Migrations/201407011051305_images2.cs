namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class images2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Listings", "ListingImageId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Listings", "ListingImageId");
        }
    }
}
