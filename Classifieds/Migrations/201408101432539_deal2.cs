namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deal2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deals", "PriceScore", c => c.Int(nullable: false));
            AddColumn("dbo.Deals", "OutreachScore", c => c.Int(nullable: false));
            AddColumn("dbo.Deals", "QualityScore", c => c.Int(nullable: false));
            AddColumn("dbo.Deals", "DurationScore", c => c.Int(nullable: false));
            AddColumn("dbo.Deals", "BulkBuyingScore", c => c.Int(nullable: false));
            AddColumn("dbo.Deals", "Votes", c => c.Int(nullable: false));
            AddColumn("dbo.Deals", "TotalScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Deals", "Hits", c => c.Int(nullable: false));
            DropColumn("dbo.Deals", "Price");
            DropColumn("dbo.Deals", "Outreach");
            DropColumn("dbo.Deals", "Condition");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deals", "Condition", c => c.Int(nullable: false));
            AddColumn("dbo.Deals", "Outreach", c => c.Int(nullable: false));
            AddColumn("dbo.Deals", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Deals", "Hits");
            DropColumn("dbo.Deals", "TotalScore");
            DropColumn("dbo.Deals", "Votes");
            DropColumn("dbo.Deals", "BulkBuyingScore");
            DropColumn("dbo.Deals", "DurationScore");
            DropColumn("dbo.Deals", "QualityScore");
            DropColumn("dbo.Deals", "OutreachScore");
            DropColumn("dbo.Deals", "PriceScore");
        }
    }
}
