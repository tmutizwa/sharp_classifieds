namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        ListingId = c.Int(nullable: false),
                        MinAge = c.Int(nullable: false),
                        MaxAge = c.Int(nullable: false),
                        Tags = c.String(),
                        MinSalary = c.Int(nullable: false),
                        MaxSalaray = c.Int(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Listings", t => t.ListingId, cascadeDelete: true)
                .Index(t => t.ListingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "ListingId", "dbo.Listings");
            DropIndex("dbo.Jobs", new[] { "ListingId" });
            DropTable("dbo.Jobs");
        }
    }
}
