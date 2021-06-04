namespace JobOffersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImageColumnInSpecializationTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specializations", "SpecializationImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Specializations", "SpecializationImage");
        }
    }
}
