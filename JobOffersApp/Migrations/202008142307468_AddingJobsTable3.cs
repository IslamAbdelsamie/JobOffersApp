namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingJobsTable3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobName = c.String(nullable: false, maxLength: 20),
                        JobDescription = c.String(nullable: false, maxLength: 50),
                        JobImage = c.String(nullable: false),
                        CategoriesId = c.Int(nullable: false),
                        SpecializationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoriesId, cascadeDelete: true)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId, cascadeDelete: true)
                .Index(t => t.CategoriesId)
                .Index(t => t.SpecializationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Jobs", "CategoriesId", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "SpecializationId" });
            DropIndex("dbo.Jobs", new[] { "CategoriesId" });
            DropTable("dbo.Jobs");
        }
    }
}
