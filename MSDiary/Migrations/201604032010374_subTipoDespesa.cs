namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subTipoDespesa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoDespesas", "subTipoDespesaNome", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TipoDespesas", "subTipoDespesaNome");
        }
    }
}
