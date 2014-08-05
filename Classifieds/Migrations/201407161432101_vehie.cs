namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vehie : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Autoes", newName: "Vehicles");
        }
        
        public override void Down()
        {
            RenameTable(name: "Autoes", newName: "Vehicles");
        }
    }
}
