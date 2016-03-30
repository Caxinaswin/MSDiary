namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class valorSaldo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Saldoes", "valor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Saldoes", "valor");
        }
    }
}
