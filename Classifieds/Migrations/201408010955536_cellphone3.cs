namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cellphone3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cellphones", "OS", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cellphones", "OS", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
