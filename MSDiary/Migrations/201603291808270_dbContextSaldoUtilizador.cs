namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbContextSaldoUtilizador : DbMigration
    {
        public override void Up()
        {
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
            DropForeignKey("dbo.SaldoUtilizadors", "ApplicationUserId", "dbo.Users");
            DropIndex("dbo.SaldoUtilizadors", new[] { "ApplicationUserId" });
            DropTable("dbo.SaldoUtilizadors");
        }
    }
}
