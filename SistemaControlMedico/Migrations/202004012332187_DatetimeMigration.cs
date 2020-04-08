namespace SistemaControlMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatetimeMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Altas", "fechaSalida", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Altas", "fechaSalida", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
