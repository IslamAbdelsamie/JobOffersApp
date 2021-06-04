namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeRelationShipWithApplicationUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Jobs", "ApplicationUserId");
            AddForeignKey("dbo.Jobs", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Jobs", "Publisher");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "Publisher", c => c.String());
            DropForeignKey("dbo.Jobs", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Jobs", new[] { "ApplicationUserId" });
            DropColumn("dbo.Jobs", "ApplicationUserId");
        }
    }
}
