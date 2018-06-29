namespace DinersClubOnline.Repositories.Campanas.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campanas",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Imagen = c.String(),
                        Banner = c.String(),
                        Prioridad = c.Int(nullable: false),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(nullable: false),
                        CreadoPor = c.String(),
                        FechaCreacion = c.DateTime(nullable: false),
                        ActualizadoPor = c.String(),
                        FechaActualizacion = c.DateTime(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.Campanas");
        }
    }
}