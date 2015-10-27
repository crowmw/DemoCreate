namespace Reository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vote", "Questionnaire_QuestionnaireId", "dbo.Questionnaire");
            DropForeignKey("dbo.Questionnaire", "Vote2_VoteId", "dbo.Vote");
            DropIndex("dbo.Questionnaire", new[] { "UserId_Id" });
            DropIndex("dbo.Questionnaire", new[] { "Vote1_VoteId" });
            DropIndex("dbo.Questionnaire", new[] { "Vote2_VoteId" });
            DropIndex("dbo.Vote", new[] { "Questionnaire_QuestionnaireId" });
            RenameColumn(table: "dbo.Questionnaire", name: "UserId_Id", newName: "User_Id");
            RenameColumn(table: "dbo.Questionnaire", name: "Vote1_VoteId", newName: "Vote_VoteId");
            AddColumn("dbo.Questionnaire", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Questionnaire", "Vote1Id", c => c.Int(nullable: false));
            AddColumn("dbo.Questionnaire", "Vote2Id", c => c.Int(nullable: false));
            AddColumn("dbo.Questionnaire", "Selection", c => c.Int(nullable: false));
            AlterColumn("dbo.Questionnaire", "Title", c => c.String());
            AlterColumn("dbo.Questionnaire", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Questionnaire", "Vote_VoteId", c => c.Int());
            CreateIndex("dbo.Questionnaire", "User_Id");
            CreateIndex("dbo.Questionnaire", "Vote_VoteId");
            DropColumn("dbo.Questionnaire", "Vote2_VoteId");
            DropColumn("dbo.Vote", "Questionnaire_QuestionnaireId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vote", "Questionnaire_QuestionnaireId", c => c.Int(nullable: false));
            AddColumn("dbo.Questionnaire", "Vote2_VoteId", c => c.Int());
            DropIndex("dbo.Questionnaire", new[] { "Vote_VoteId" });
            DropIndex("dbo.Questionnaire", new[] { "User_Id" });
            AlterColumn("dbo.Questionnaire", "Vote_VoteId", c => c.Int(nullable: false));
            AlterColumn("dbo.Questionnaire", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Questionnaire", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Questionnaire", "Selection");
            DropColumn("dbo.Questionnaire", "Vote2Id");
            DropColumn("dbo.Questionnaire", "Vote1Id");
            DropColumn("dbo.Questionnaire", "UserId");
            RenameColumn(table: "dbo.Questionnaire", name: "Vote_VoteId", newName: "Vote1_VoteId");
            RenameColumn(table: "dbo.Questionnaire", name: "User_Id", newName: "UserId_Id");
            CreateIndex("dbo.Vote", "Questionnaire_QuestionnaireId");
            CreateIndex("dbo.Questionnaire", "Vote2_VoteId");
            CreateIndex("dbo.Questionnaire", "Vote1_VoteId");
            CreateIndex("dbo.Questionnaire", "UserId_Id");
            AddForeignKey("dbo.Questionnaire", "Vote2_VoteId", "dbo.Vote", "VoteId");
            AddForeignKey("dbo.Vote", "Questionnaire_QuestionnaireId", "dbo.Questionnaire", "QuestionnaireId");
        }
    }
}
