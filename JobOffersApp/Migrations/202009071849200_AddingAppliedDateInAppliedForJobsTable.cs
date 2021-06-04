namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAppliedDateInAppliedForJobsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplyForJobs", "AppliedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplyForJobs", "AppliedDate");
        }
    }
}
