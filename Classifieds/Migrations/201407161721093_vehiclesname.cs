namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vehiclesname : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Cars", newName: "Vehicles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Vehicles", newName: "Cars");
        }
    }
}
