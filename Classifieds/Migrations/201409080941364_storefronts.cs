namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storefronts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Storefronts",
                c => new
                    {
                        StorefrontId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Logo = c.String(),
                    })
                .PrimaryKey(t => t.StorefrontId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Storefronts");
        }
    }
}
