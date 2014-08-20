namespace Classifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        SenderName = c.String(),
                        SenderId = c.String(),
                        ListingId = c.Int(nullable: false),
                        Detail = c.String(),
                        SenderEmail = c.String(),
                        Phone = c.String(),
                        Subject = c.String(),
                    })
                .PrimaryKey(t => t.MessageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Messages");
        }
    }
}
