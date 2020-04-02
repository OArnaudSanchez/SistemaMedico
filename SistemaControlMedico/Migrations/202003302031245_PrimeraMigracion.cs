namespace SistemaControlMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeraMigracion : DbMigration
    {
        public override void Up()
        {


            CreateTable(
                "dbo.Habitaciones",
                c => new
                {
                    idHabitacion = c.Int(nullable: false, identity: true),
                    numero = c.Int(nullable: false),
                    tipo = c.String(maxLength: 25, unicode: false),
                    fechaIngreso = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.idHabitacion);



        }
        
        public override void Down()
        {
            
            DropTable("dbo.Habitaciones");
           
        }
    }
}
