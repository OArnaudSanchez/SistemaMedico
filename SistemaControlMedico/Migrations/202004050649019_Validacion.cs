namespace SistemaControlMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validacion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Habitaciones", "tipo", c => c.String(nullable: false, maxLength: 25, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Habitaciones", "tipo", c => c.String(maxLength: 25, unicode: false));
        }
    }
}
