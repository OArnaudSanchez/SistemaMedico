namespace SistemaControlMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiandoDatos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pacientes", "asegurado", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pacientes", "asegurado", c => c.Boolean(nullable: false));
        }
    }
}
