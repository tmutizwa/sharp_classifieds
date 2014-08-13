namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class features_user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeatureRequests", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FeatureRequests", "UserId");
        }
    }
}
