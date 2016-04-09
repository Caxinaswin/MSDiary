namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                .ForeignKey("dbo.Users", t => t.ApplicationUserId)
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
                        subTipoDespesaId = c.Int(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TipoDespesaId)
                .ForeignKey("dbo.TipoDespesas", t => t.subTipoDespesaId)
                .ForeignKey("dbo.Users", t => t.ApplicationUserId)
                .Index(t => t.subTipoDespesaId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
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
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.User_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
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
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TipoPagamentoes",
                c => new
                    {
                        TipoPagamentoId = c.Int(nullable: false, identity: true),
                        TipoPagamentoNome = c.String(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TipoPagamentoId)
                .ForeignKey("dbo.Users", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
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
                        ApplicationUserId = c.String(maxLength: 128),
                        Saldo_SaldoId = c.Int(),
                    })
                .PrimaryKey(t => t.RendimentoId)
                .ForeignKey("dbo.TipoRendimentoes", t => t.TipoRendimentoId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ApplicationUserId)
                .ForeignKey("dbo.Saldoes", t => t.Saldo_SaldoId)
                .Index(t => t.TipoRendimentoId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.Saldo_SaldoId);
            
            CreateTable(
                "dbo.TipoRendimentoes",
                c => new
                    {
                        TipoRendimentoId = c.Int(nullable: false, identity: true),
                        TipoRendimentoNome = c.String(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TipoRendimentoId)
                .ForeignKey("dbo.Users", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
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
                "dbo.Saldoes",
                c => new
                    {
                        SaldoId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(),
                        valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SaldoId);
            
            CreateTable(
                "dbo.SaldoUtilizadors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        data = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        despesaId = c.Int(nullable: false),
                        rendimentoId = c.Int(nullable: false),
                        valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.SaldoUtilizadors", "ApplicationUserId", "dbo.Users");
            DropForeignKey("dbo.Rendimentoes", "Saldo_SaldoId", "dbo.Saldoes");
            DropForeignKey("dbo.Despesas", "Saldo_SaldoId", "dbo.Saldoes");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Rendimentoes", "ApplicationUserId", "dbo.Users");
            DropForeignKey("dbo.Rendimentoes", "TipoRendimentoId", "dbo.TipoRendimentoes");
            DropForeignKey("dbo.TipoRendimentoes", "ApplicationUserId", "dbo.Users");
            DropForeignKey("dbo.Despesas", "ApplicationUserId", "dbo.Users");
            DropForeignKey("dbo.Despesas", "TipoPagamentoId", "dbo.TipoPagamentoes");
            DropForeignKey("dbo.TipoPagamentoes", "ApplicationUserId", "dbo.Users");
            DropForeignKey("dbo.Despesas", "TipoDespesaId", "dbo.TipoDespesas");
            DropForeignKey("dbo.TipoDespesas", "ApplicationUserId", "dbo.Users");
            DropForeignKey("dbo.TipoDespesas", "subTipoDespesaId", "dbo.TipoDespesas");
            DropIndex("dbo.SaldoUtilizadors", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TipoRendimentoes", new[] { "ApplicationUserId" });
            DropIndex("dbo.Rendimentoes", new[] { "Saldo_SaldoId" });
            DropIndex("dbo.Rendimentoes", new[] { "ApplicationUserId" });
            DropIndex("dbo.Rendimentoes", new[] { "TipoRendimentoId" });
            DropIndex("dbo.TipoPagamentoes", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.TipoDespesas", new[] { "ApplicationUserId" });
            DropIndex("dbo.TipoDespesas", new[] { "subTipoDespesaId" });
            DropIndex("dbo.Despesas", new[] { "Saldo_SaldoId" });
            DropIndex("dbo.Despesas", new[] { "ApplicationUserId" });
            DropIndex("dbo.Despesas", new[] { "TipoPagamentoId" });
            DropIndex("dbo.Despesas", new[] { "TipoDespesaId" });
            DropTable("dbo.SaldoUtilizadors");
            DropTable("dbo.Saldoes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TipoRendimentoes");
            DropTable("dbo.Rendimentoes");
            DropTable("dbo.TipoPagamentoes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.TipoDespesas");
            DropTable("dbo.Despesas");
        }
    }
}
