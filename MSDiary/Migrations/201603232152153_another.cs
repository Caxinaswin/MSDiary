namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class another : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "Users");
            RenameColumn(table: "dbo.Users", name: "Id", newName: "User_Id");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Users", name: "User_Id", newName: "Id");
            RenameTable(name: "dbo.Users", newName: "AspNetUsers");
        }
    }
}
