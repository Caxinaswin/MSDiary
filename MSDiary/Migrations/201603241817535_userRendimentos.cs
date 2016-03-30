namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userRendimentos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rendimentoes", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Rendimentoes", "ApplicationUserId");
            AddForeignKey("dbo.Rendimentoes", "ApplicationUserId", "dbo.Users", "User_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rendimentoes", "ApplicationUserId", "dbo.Users");
            DropIndex("dbo.Rendimentoes", new[] { "ApplicationUserId" });
            DropColumn("dbo.Rendimentoes", "ApplicationUserId");
        }
    }
}
