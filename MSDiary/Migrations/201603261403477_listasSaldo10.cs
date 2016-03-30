namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listasSaldo10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Saldoes", "DespesaId", c => c.Int(nullable: false));
            AddColumn("dbo.Saldoes", "RendimentoId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Saldoes", "RendimentoId");
            DropColumn("dbo.Saldoes", "DespesaId");
        }
    }
}
