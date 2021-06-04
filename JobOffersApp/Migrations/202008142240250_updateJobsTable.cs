namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateJobsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "Categories_Id", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "Categories_Id" });
            DropColumn("dbo.Jobs", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "CategoryId", c => c.Int(nullable: false));
        }
    }
}
