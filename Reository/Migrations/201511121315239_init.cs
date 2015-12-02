namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Choose",
                c => new
                    {
                        ChooseId = c.Guid(nullable: false),
                        VoteId = c.Guid(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ChooseId)
                .ForeignKey("dbo.Vote", t => t.VoteId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.VoteId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        RegisterDateTime = c.DateTime(),
                        AvatarPath = c.String(),
                        ProvinceId = c.Int(),
                        Gender = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ProvinceId = c.Int(nullable: false, identity: true),
                        ProvinceName = c.String(),
                    })
                .PrimaryKey(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Questionnaire",
                c => new
                    {
                        QuestionnaireId = c.Guid(nullable: false),
                        Title = c.String(),
                        TimeOfCreation = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Vote1Id = c.Guid(),
                        Vote2Id = c.Guid(),
                    })
                .PrimaryKey(t => t.QuestionnaireId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Vote", t => t.Vote1Id)
                .ForeignKey("dbo.Vote", t => t.Vote2Id)
                .Index(t => t.UserId)
                .Index(t => t.Vote1Id)
                .Index(t => t.Vote2Id);
            
            CreateTable(
                "dbo.Vote",
                c => new
                    {
                        VoteId = c.Guid(nullable: false),
                        Image = c.String(),
                        VoteTitle = c.String(nullable: false),
                        QuestionnaireId = c.Guid(),
                        Questionnaire_QuestionnaireId = c.Guid(),
                    })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.Questionnaire", t => t.Questionnaire_QuestionnaireId)
                .Index(t => t.Questionnaire_QuestionnaireId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.User_Questionnaire",
                c => new
                    {
                        User_QuestionnaireId = c.Guid(nullable: false),
                        UserId = c.String(),
                        QuestionnaireId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.User_QuestionnaireId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Choose", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questionnaire", "Vote2Id", "dbo.Vote");
            DropForeignKey("dbo.Questionnaire", "Vote1Id", "dbo.Vote");
            DropForeignKey("dbo.Choose", "VoteId", "dbo.Vote");
            DropForeignKey("dbo.Vote", "Questionnaire_QuestionnaireId", "dbo.Questionnaire");
            DropForeignKey("dbo.Questionnaire", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ProvinceId", "dbo.Provinces");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Vote", new[] { "Questionnaire_QuestionnaireId" });
            DropIndex("dbo.Questionnaire", new[] { "Vote2Id" });
            DropIndex("dbo.Questionnaire", new[] { "Vote1Id" });
            DropIndex("dbo.Questionnaire", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "ProvinceId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Choose", new[] { "UserId" });
            DropIndex("dbo.Choose", new[] { "VoteId" });
            DropTable("dbo.User_Questionnaire");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Vote");
            DropTable("dbo.Questionnaire");
            DropTable("dbo.Provinces");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Choose");
        }
    }
}
