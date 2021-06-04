namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPublisherInJobsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Publisher", c => c.String());
            AddColumn("dbo.Jobs", "PublishDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "PublishDate");
            DropColumn("dbo.Jobs", "Publisher");
        }
    }
}
