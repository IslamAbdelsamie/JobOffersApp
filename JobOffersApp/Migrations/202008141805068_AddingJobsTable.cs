namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingJobsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobName = c.String(nullable: false, maxLength: 25),
                        JobDescription = c.String(nullable: false, maxLength: 50),
                        JobImage = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        SpecializationId = c.Int(nullable: false),
                        Categories_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Categories_Id)
                .ForeignKey("dbo.Specializations", t => t.SpecializationId, cascadeDelete: true)
                .Index(t => t.SpecializationId)
                .Index(t => t.Categories_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.Jobs", "Categories_Id", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "Categories_Id" });
            DropIndex("dbo.Jobs", new[] { "SpecializationId" });
            DropTable("dbo.Jobs");
        }
    }
}
