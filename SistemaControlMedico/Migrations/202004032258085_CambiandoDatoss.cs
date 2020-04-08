namespace SistemaControlMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiandoDatoss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pacientes", "asegurado", c => c.String(nullable: false, maxLength: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pacientes", "asegurado", c => c.String(nullable: false));
        }
    }
}
