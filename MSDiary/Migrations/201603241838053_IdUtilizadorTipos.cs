namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdUtilizadorTipos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoDespesas", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.TipoPagamentoes", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.TipoRendimentoes", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TipoDespesas", "ApplicationUserId");
            CreateIndex("dbo.TipoPagamentoes", "ApplicationUserId");
            CreateIndex("dbo.TipoRendimentoes", "ApplicationUserId");
            AddForeignKey("dbo.TipoDespesas", "ApplicationUserId", "dbo.Users", "User_Id");
            AddForeignKey("dbo.TipoPagamentoes", "ApplicationUserId", "dbo.Users", "User_Id");
            AddForeignKey("dbo.TipoRendimentoes", "ApplicationUserId", "dbo.Users", "User_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TipoRendimentoes", "ApplicationUserId", "dbo.Users");
            DropForeignKey("dbo.TipoPagamentoes", "ApplicationUserId", "dbo.Users");
            DropForeignKey("dbo.TipoDespesas", "ApplicationUserId", "dbo.Users");
            DropIndex("dbo.TipoRendimentoes", new[] { "ApplicationUserId" });
            DropIndex("dbo.TipoPagamentoes", new[] { "ApplicationUserId" });
            DropIndex("dbo.TipoDespesas", new[] { "ApplicationUserId" });
            DropColumn("dbo.TipoRendimentoes", "ApplicationUserId");
            DropColumn("dbo.TipoPagamentoes", "ApplicationUserId");
            DropColumn("dbo.TipoDespesas", "ApplicationUserId");
        }
    }
}
