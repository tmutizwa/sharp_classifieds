namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cars : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cars", "Mileage");
            AddColumn("dbo.Cars", "EngineSize", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Cars", "Mileage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "Mileage", c => c.String());
            DropColumn("dbo.Cars", "EngineSize");
        }
    }
}
