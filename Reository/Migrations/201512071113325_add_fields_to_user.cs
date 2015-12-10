namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_fields_to_user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AgeRangeId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "EducationId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "AgeRangeId");
            CreateIndex("dbo.AspNetUsers", "EducationId");
            AddForeignKey("dbo.AspNetUsers", "AgeRangeId", "dbo.AgeRange", "AgeRangeId");
            AddForeignKey("dbo.AspNetUsers", "EducationId", "dbo.Education", "EducationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "EducationId", "dbo.Education");
            DropForeignKey("dbo.AspNetUsers", "AgeRangeId", "dbo.AgeRange");
            DropIndex("dbo.AspNetUsers", new[] { "EducationId" });
            DropIndex("dbo.AspNetUsers", new[] { "AgeRangeId" });
            DropColumn("dbo.AspNetUsers", "EducationId");
            DropColumn("dbo.AspNetUsers", "AgeRangeId");
        }
    }
}
