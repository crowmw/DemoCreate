namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_age_education : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgeRange",
                c => new
                    {
                        AgeRangeId = c.Int(nullable: false, identity: true),
                        AgeRangeName = c.String(),
                    })
                .PrimaryKey(t => t.AgeRangeId);
            
            CreateTable(
                "dbo.Education",
                c => new
                    {
                        EducationId = c.Int(nullable: false, identity: true),
                        EducationName = c.String(),
                    })
                .PrimaryKey(t => t.EducationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Education");
            DropTable("dbo.AgeRange");
        }
    }
}
