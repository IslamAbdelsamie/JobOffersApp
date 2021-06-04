namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteJobsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "Categories_Id", "dbo.Categories");
            DropForeignKey("dbo.Jobs", "SpecializationId", "dbo.Specializations");
            DropIndex("dbo.Jobs", new[] { "SpecializationId" });
            DropIndex("dbo.Jobs", new[] { "Categories_Id" });
            DropTable("dbo.Jobs");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Jobs", "Categories_Id");
            CreateIndex("dbo.Jobs", "SpecializationId");
            AddForeignKey("dbo.Jobs", "SpecializationId", "dbo.Specializations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "Categories_Id", "dbo.Categories", "Id");
        }
    }
}
