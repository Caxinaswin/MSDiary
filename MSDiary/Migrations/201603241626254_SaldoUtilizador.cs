namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaldoUtilizador : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Saldoes", "ApplicationUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Saldoes", "ApplicationUserId");
        }
    }
}
