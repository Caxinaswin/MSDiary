namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subtTipoV5 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TipoDespesas", new[] { "subTipoDespesaId" });
            AlterColumn("dbo.TipoDespesas", "subTipoDespesaId", c => c.Int());
            CreateIndex("dbo.TipoDespesas", "subTipoDespesaId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TipoDespesas", new[] { "subTipoDespesaId" });
            AlterColumn("dbo.TipoDespesas", "subTipoDespesaId", c => c.Int(nullable: false));
            CreateIndex("dbo.TipoDespesas", "subTipoDespesaId");
        }
    }
}
