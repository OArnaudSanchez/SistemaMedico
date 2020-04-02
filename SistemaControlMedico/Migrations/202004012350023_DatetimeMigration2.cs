namespace SistemaControlMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatetimeMigration2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ingresos", "fechaIngreso", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ingresos", "fechaIngreso", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
