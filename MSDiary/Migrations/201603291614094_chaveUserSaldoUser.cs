namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chaveUserSaldoUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Saldoes", "saldo_SaldoId", "dbo.Saldoes");
            DropIndex("dbo.Saldoes", new[] { "saldo_SaldoId" });
            DropColumn("dbo.Saldoes", "saldo_SaldoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Saldoes", "saldo_SaldoId", c => c.Int());
            CreateIndex("dbo.Saldoes", "saldo_SaldoId");
            AddForeignKey("dbo.Saldoes", "saldo_SaldoId", "dbo.Saldoes", "SaldoId");
        }
    }
}
