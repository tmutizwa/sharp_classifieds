namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cellphones", "CModel", c => c.String());
            DropColumn("dbo.Cellphones", "Model");
            DropColumn("dbo.Cellphones", "Warranty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cellphones", "Warranty", c => c.String());
            AddColumn("dbo.Cellphones", "Model", c => c.String());
            DropColumn("dbo.Cellphones", "CModel");
        }
    }
}
