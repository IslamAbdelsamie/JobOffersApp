namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSlugColumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "CategorySlug", c => c.String(maxLength: 20));
            AlterColumn("dbo.Specializations", "SpecializationSlug", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Specializations", "SpecializationSlug", c => c.String());
            AlterColumn("dbo.Categories", "CategorySlug", c => c.String());
        }
    }
}
