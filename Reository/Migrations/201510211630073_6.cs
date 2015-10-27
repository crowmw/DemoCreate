namespace Reository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Questionnaire", new[] { "User_Id" });
            DropIndex("dbo.Questionnaire", new[] { "Vote_VoteId" });
            RenameColumn(table: "dbo.Questionnaire", name: "User_Id", newName: "UserId_Id");
            RenameColumn(table: "dbo.Questionnaire", name: "Vote_VoteId", newName: "Vote1_VoteId");
            AddColumn("dbo.Questionnaire", "Vote2_VoteId", c => c.Int());
            AddColumn("dbo.Vote", "Questionnaire_QuestionnaireId", c => c.Int(nullable: false));
            AlterColumn("dbo.Questionnaire", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Questionnaire", "UserId_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Questionnaire", "Vote1_VoteId", c => c.Int(nullable: false));
            CreateIndex("dbo.Questionnaire", "UserId_Id");
            CreateIndex("dbo.Questionnaire", "Vote1_VoteId");
            CreateIndex("dbo.Questionnaire", "Vote2_VoteId");
            CreateIndex("dbo.Vote", "Questionnaire_QuestionnaireId");
            AddForeignKey("dbo.Vote", "Questionnaire_QuestionnaireId", "dbo.Questionnaire", "QuestionnaireId");
            AddForeignKey("dbo.Questionnaire", "Vote2_VoteId", "dbo.Vote", "VoteId");
            DropColumn("dbo.Questionnaire", "UserId");
            DropColumn("dbo.Questionnaire", "Vote1Id");
            DropColumn("dbo.Questionnaire", "Vote2Id");
            DropColumn("dbo.Questionnaire", "Selection");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questionnaire", "Selection", c => c.Int(nullable: false));
            AddColumn("dbo.Questionnaire", "Vote2Id", c => c.Int(nullable: false));
            AddColumn("dbo.Questionnaire", "Vote1Id", c => c.Int(nullable: false));
            AddColumn("dbo.Questionnaire", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Questionnaire", "Vote2_VoteId", "dbo.Vote");
            DropForeignKey("dbo.Vote", "Questionnaire_QuestionnaireId", "dbo.Questionnaire");
            DropIndex("dbo.Vote", new[] { "Questionnaire_QuestionnaireId" });
            DropIndex("dbo.Questionnaire", new[] { "Vote2_VoteId" });
            DropIndex("dbo.Questionnaire", new[] { "Vote1_VoteId" });
            DropIndex("dbo.Questionnaire", new[] { "UserId_Id" });
            AlterColumn("dbo.Questionnaire", "Vote1_VoteId", c => c.Int());
            AlterColumn("dbo.Questionnaire", "UserId_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Questionnaire", "Title", c => c.String());
            DropColumn("dbo.Vote", "Questionnaire_QuestionnaireId");
            DropColumn("dbo.Questionnaire", "Vote2_VoteId");
            RenameColumn(table: "dbo.Questionnaire", name: "Vote1_VoteId", newName: "Vote_VoteId");
            RenameColumn(table: "dbo.Questionnaire", name: "UserId_Id", newName: "User_Id");
            CreateIndex("dbo.Questionnaire", "Vote_VoteId");
            CreateIndex("dbo.Questionnaire", "User_Id");
        }
    }
}
