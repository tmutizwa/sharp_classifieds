namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class salary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "MaxSalary", c => c.Int(nullable: false));
            DropColumn("dbo.Jobs", "MaxSalaray");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "MaxSalaray", c => c.Int(nullable: false));
            DropColumn("dbo.Jobs", "MaxSalary");
        }
    }
}
