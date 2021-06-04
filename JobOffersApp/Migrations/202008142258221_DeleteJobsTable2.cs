namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteJobsTable2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "SpecializationId", "dbo.Specializations");
            DropIndex("dbo.Jobs", new[] { "SpecializationId" });
            DropTable("dbo.Jobs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobName = c.String(nullable: false, maxLength: 20),
                        JobDescription = c.String(nullable: false, maxLength: 50),
                        JobImage = c.String(nullable: false),
                        SpecializationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Jobs", "SpecializationId");
            AddForeignKey("dbo.Jobs", "SpecializationId", "dbo.Specializations", "Id", cascadeDelete: true);
        }
    }
}
