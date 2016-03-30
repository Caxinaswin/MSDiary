namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class saldoObjeto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Saldoes", "saldo_SaldoId", c => c.Int());
            CreateIndex("dbo.Saldoes", "saldo_SaldoId");
            AddForeignKey("dbo.Saldoes", "saldo_SaldoId", "dbo.Saldoes", "SaldoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Saldoes", "saldo_SaldoId", "dbo.Saldoes");
            DropIndex("dbo.Saldoes", new[] { "saldo_SaldoId" });
            DropColumn("dbo.Saldoes", "saldo_SaldoId");
        }
    }
}
