namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bugUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bugs", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bugs", "UserId");
        }
    }
}
