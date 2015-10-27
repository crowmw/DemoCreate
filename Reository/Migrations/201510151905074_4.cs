namespace Reository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "LoginProviderKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "LoginProviderKey", c => c.String());
        }
    }
}
