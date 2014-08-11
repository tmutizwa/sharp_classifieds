namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class totlascrore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deals", "TotalScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deals", "TotalScore");
        }
    }
}
