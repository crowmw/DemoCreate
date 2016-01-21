namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reqTitle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questionnaire", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questionnaire", "Title", c => c.String());
        }
    }
}
