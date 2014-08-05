namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cellphones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cellphones",
                c => new
                    {
                        CellphoneId = c.Int(nullable: false, identity: true),
                        OS = c.String(nullable: false, maxLength: 100),
                        Brand = c.String(),
                        Model = c.String(),
                        ScreenSize = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetworkType = c.String(),
                        Condition = c.String(),
                        Warranty = c.String(),
                    })
                .PrimaryKey(t => t.CellphoneId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cellphones");
        }
    }
}
