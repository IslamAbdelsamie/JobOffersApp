namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCVsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CVsTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        CV = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CVsTables", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CVsTables", new[] { "ApplicationUserId" });
            DropTable("dbo.CVsTables");
        }
    }
}
