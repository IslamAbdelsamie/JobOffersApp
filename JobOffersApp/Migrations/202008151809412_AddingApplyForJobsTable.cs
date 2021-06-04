namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingApplyForJobsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyForJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobMessage = c.String(nullable: false, maxLength: 50),
                        JobsId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Jobs", t => t.JobsId, cascadeDelete: true)
                .Index(t => t.JobsId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyForJobs", "JobsId", "dbo.Jobs");
            DropForeignKey("dbo.ApplyForJobs", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ApplyForJobs", new[] { "ApplicationUserId" });
            DropIndex("dbo.ApplyForJobs", new[] { "JobsId" });
            DropTable("dbo.ApplyForJobs");
        }
    }
}
