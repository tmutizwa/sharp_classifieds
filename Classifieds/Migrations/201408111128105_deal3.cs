namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deal3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Deals", "TotalScore");
            DropColumn("dbo.Deals", "Hits");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deals", "Hits", c => c.Int(nullable: false));
            AddColumn("dbo.Deals", "TotalScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
