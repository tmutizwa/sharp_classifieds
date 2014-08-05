namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autosname : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Autoes", newName: "Vehicles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Vehicles", newName: "Autoes");
        }
    }
}
