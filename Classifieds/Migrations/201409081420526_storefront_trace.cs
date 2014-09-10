namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storefront_trace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Storefronts", "OwnerId", c => c.String(maxLength: 128));
            AddColumn("dbo.Storefronts", "UpdaterId", c => c.String());
            AddColumn("dbo.Storefronts", "Status", c => c.String());
            AddColumn("dbo.Storefronts", "Created", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Storefronts", "Updated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Storefronts", "Expires", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.Storefronts", "OwnerId");
            AddForeignKey("dbo.Storefronts", "OwnerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Storefronts", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.Storefronts", new[] { "OwnerId" });
            DropColumn("dbo.Storefronts", "Expires");
            DropColumn("dbo.Storefronts", "Updated");
            DropColumn("dbo.Storefronts", "Created");
            DropColumn("dbo.Storefronts", "Status");
            DropColumn("dbo.Storefronts", "UpdaterId");
            DropColumn("dbo.Storefronts", "OwnerId");
        }
    }
}
