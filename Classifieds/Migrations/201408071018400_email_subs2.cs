namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class email_subs2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailSubscriptions", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.EmailSubscriptions", "UserId");
            AddForeignKey("dbo.EmailSubscriptions", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailSubscriptions", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.EmailSubscriptions", new[] { "UserId" });
            DropColumn("dbo.EmailSubscriptions", "UserId");
        }
    }
}
