namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bugreport_featureRequests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bugs",
                c => new
                    {
                        BugId = c.Int(nullable: false, identity: true),
                        Detail = c.String(),
                        Title = c.String(),
                        Email = c.String(),
                        Created = c.DateTime(precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.BugId);
            
            CreateTable(
                "dbo.FeatureRequests",
                c => new
                    {
                        FeatureRequestId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Detail = c.String(),
                        Email = c.String(),
                        Created = c.DateTime(precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.FeatureRequestId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FeatureRequests");
            DropTable("dbo.Bugs");
        }
    }
}
