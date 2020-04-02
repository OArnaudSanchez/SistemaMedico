namespace SistemaControlMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SegundaMigracion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Habitaciones", "precioDia", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Habitaciones", "precioDia");
        }
    }
}
