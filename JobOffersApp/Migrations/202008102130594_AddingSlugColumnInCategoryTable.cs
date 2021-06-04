namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSlugColumnInCategoryTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CategorySlug", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "CategorySlug");
        }
    }
}
