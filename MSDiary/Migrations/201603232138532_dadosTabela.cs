namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dadosTabela : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Despesas",
                c => new
                    {
                        DespesaId = c.Int(nullable: false, identity: true),
                        TipoDespesaId = c.Int(nullable: false),
                        DespesaDescricao = c.String(nullable: false),
                        DespesaValor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TipoPagamentoId = c.Int(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Comentario = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        Saldo_SaldoId = c.Int(),
                    })
                .PrimaryKey(t => t.DespesaId)
                .ForeignKey("dbo.TipoDespesas", t => t.TipoDespesaId, cascadeDelete: true)
                .ForeignKey("dbo.TipoPagamentoes", t => t.TipoPagamentoId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Saldoes", t => t.Saldo_SaldoId)
                .Index(t => t.TipoDespesaId)
                .Index(t => t.TipoPagamentoId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.Saldo_SaldoId);
            
            CreateTable(
                "dbo.TipoDespesas",
                c => new
                    {
                        TipoDespesaId = c.Int(nullable: false, identity: true),
                        TipoDespesaNome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TipoDespesaId);
            
            CreateTable(
                "dbo.TipoPagamentoes",
                c => new
                    {
                        TipoPagamentoId = c.Int(nullable: false, identity: true),
                        TipoPagamentoNome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TipoPagamentoId);

            CreateTable(
                "dbo.TipoRendimentoes",
                c => new
                {
                    TipoRendimentoId = c.Int(nullable: false, identity: true),
                    TipoRendimentoNome = c.String(nullable: false),
                })
                .PrimaryKey(t => t.TipoRendimentoId);

            CreateTable(
                "dbo.Rendimentoes",
                c => new
                    {
                        RendimentoId = c.Int(nullable: false, identity: true),
                        TipoRendimentoId = c.Int(nullable: false),
                        RendimentoDescricao = c.String(nullable: false),
                        RendimentoValor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        Comentario = c.String(),
                        Saldo_SaldoId = c.Int(),
                    })
                .PrimaryKey(t => t.RendimentoId)
                .ForeignKey("dbo.TipoRendimentoes", t => t.TipoRendimentoId, cascadeDelete: true)
                .ForeignKey("dbo.Saldoes", t => t.Saldo_SaldoId)
                .Index(t => t.TipoRendimentoId)
                .Index(t => t.Saldo_SaldoId);
            
            CreateTable(
                "dbo.Saldoes",
                c => new
                    {
                        SaldoId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.SaldoId);
            

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rendimentoes", "Saldo_SaldoId", "dbo.Saldoes");
            DropForeignKey("dbo.Despesas", "Saldo_SaldoId", "dbo.Saldoes");
            DropForeignKey("dbo.Rendimentoes", "TipoRendimentoId", "dbo.TipoRendimentoes");
            DropForeignKey("dbo.Despesas", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Despesas", "TipoPagamentoId", "dbo.TipoPagamentoes");
            DropForeignKey("dbo.Despesas", "TipoDespesaId", "dbo.TipoDespesas");
            DropIndex("dbo.Rendimentoes", new[] { "Saldo_SaldoId" });
            DropIndex("dbo.Rendimentoes", new[] { "TipoRendimentoId" });
            DropIndex("dbo.Despesas", new[] { "Saldo_SaldoId" });
            DropIndex("dbo.Despesas", new[] { "ApplicationUserId" });
            DropIndex("dbo.Despesas", new[] { "TipoPagamentoId" });
            DropIndex("dbo.Despesas", new[] { "TipoDespesaId" });
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropTable("dbo.Saldoes");
            DropTable("dbo.TipoRendimentoes");
            DropTable("dbo.Rendimentoes");
            DropTable("dbo.TipoPagamentoes");
            DropTable("dbo.TipoDespesas");
            DropTable("dbo.Despesas");
        }
    }
}
