namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class email_subs1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailSubscriptions",
                c => new
                    {
                        EmailSubscriptionId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Name = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Period = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmailSubscriptionId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailSubscriptions", "CategoryId", "dbo.Categories");
            DropIndex("dbo.EmailSubscriptions", new[] { "CategoryId" });
            DropTable("dbo.EmailSubscriptions");
        }
    }
}
