namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class car : DbMigration
    {
        public override void Up()
        {
           // RenameTable(name: "dbo.Autoes", newName: "Cars");
            RenameTable("[dbo].[dbo.Autoes]", "Cars");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Cars", newName: "Vehicles");
        }
    }
}
