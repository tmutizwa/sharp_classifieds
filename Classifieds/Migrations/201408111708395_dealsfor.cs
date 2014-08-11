namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dealsfor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deals", "UpdaterId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Deals", "UpdaterId");
            AddForeignKey("dbo.Deals", "UpdaterId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deals", "UpdaterId", "dbo.AspNetUsers");
            DropIndex("dbo.Deals", new[] { "UpdaterId" });
            DropColumn("dbo.Deals", "UpdaterId");
        }
    }
}
