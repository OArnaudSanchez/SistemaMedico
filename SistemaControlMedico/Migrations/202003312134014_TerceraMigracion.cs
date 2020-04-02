namespace SistemaControlMedico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TerceraMigracion : DbMigration
    {
        public override void Up()
        {
           // DropColumn("dbo.Altas", "valor");
        }

        public override void Down()
        {
        //    AddColumn("dbo.Altas", "valor", c => c.String());
        }
    }
}
