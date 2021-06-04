namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCVIdToApplyForJobsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplyForJobs", "CVsTableId", c => c.Int(nullable: true));
            CreateIndex("dbo.ApplyForJobs", "CVsTableId");
            AddForeignKey("dbo.ApplyForJobs", "CVsTableId", "dbo.CVsTables", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyForJobs", "CVsTableId", "dbo.CVsTables");
            DropIndex("dbo.ApplyForJobs", new[] { "CVsTableId" });
            DropColumn("dbo.ApplyForJobs", "CVsTableId");
        }
    }
}
