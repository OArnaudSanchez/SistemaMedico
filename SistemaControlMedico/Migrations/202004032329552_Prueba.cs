namespace SistemaControlMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prueba : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Citas", "fecha", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Citas", "fecha", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
