namespace MSDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataTimeCanbeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SaldoUtilizadors", "data", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SaldoUtilizadors", "data", c => c.DateTime(nullable: false));
        }
    }
}
