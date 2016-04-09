namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class despesaRendimentoNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SaldoUtilizadors", "despesaId", c => c.Int());
            AlterColumn("dbo.SaldoUtilizadors", "rendimentoId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SaldoUtilizadors", "rendimentoId", c => c.Int(nullable: false));
            AlterColumn("dbo.SaldoUtilizadors", "despesaId", c => c.Int(nullable: false));
        }
    }
}
