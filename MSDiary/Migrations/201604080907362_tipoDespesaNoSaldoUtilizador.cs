namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tipoDespesaNoSaldoUtilizador : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SaldoUtilizadors", "TipoDespesaId", c => c.Int());
            AddColumn("dbo.SaldoUtilizadors", "TipoRendimentoId", c => c.Int());
            AddColumn("dbo.SaldoUtilizadors", "tipoPagamento_TipoPagamentoId", c => c.Int());
            CreateIndex("dbo.SaldoUtilizadors", "TipoDespesaId");
            CreateIndex("dbo.SaldoUtilizadors", "tipoPagamento_TipoPagamentoId");
            AddForeignKey("dbo.SaldoUtilizadors", "TipoDespesaId", "dbo.TipoDespesas", "TipoDespesaId");
            AddForeignKey("dbo.SaldoUtilizadors", "tipoPagamento_TipoPagamentoId", "dbo.TipoPagamentoes", "TipoPagamentoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaldoUtilizadors", "tipoPagamento_TipoPagamentoId", "dbo.TipoPagamentoes");
            DropForeignKey("dbo.SaldoUtilizadors", "TipoDespesaId", "dbo.TipoDespesas");
            DropIndex("dbo.SaldoUtilizadors", new[] { "tipoPagamento_TipoPagamentoId" });
            DropIndex("dbo.SaldoUtilizadors", new[] { "TipoDespesaId" });
            DropColumn("dbo.SaldoUtilizadors", "tipoPagamento_TipoPagamentoId");
            DropColumn("dbo.SaldoUtilizadors", "TipoRendimentoId");
            DropColumn("dbo.SaldoUtilizadors", "TipoDespesaId");
        }
    }
}
