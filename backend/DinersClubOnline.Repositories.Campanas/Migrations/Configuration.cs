using System;
using System.Data.Entity.Migrations;
using DinersClubOnline.Model.Campanas;

namespace DinersClubOnline.Repositories.Campanas.Migrations
{


    internal sealed class Configuration : DbMigrationsConfiguration<CampanaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DinersClubOnline.Repositories.Campanas.CampanaContext";
        }

        protected override void Seed(CampanaContext context)
        {
            context.Campanas.AddOrUpdate(
            new Campana
            {
                Activo = true,
                ActualizadoPor = "admin",
                Banner = @"<span><i class=""icon1-bag""></i></span>
                                    <div class=""section__body-estilo1"">
                                        <ul>
                                            <li class=""list_first""><small>Sáb 16 Jul</small> </li>
                                            <li>
                                                <div class=""label__titulo1"">
                                                    <small><b>Shopping day<br />
                                                        Diners Club</b></small>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>",
                CreadoPor = "admin",
                Descripcion = "campana01",
                FechaActualizacion = DateTime.Today,
                FechaCreacion = DateTime.Today,
                FechaFin = DateTime.Parse("2016-12-31 00:00:00.000"),
                FechaInicio = DateTime.Parse("2016-01-01 00:00:00.000"),
                Id = "5a822240-5077-43a9-a120-600c31e7b642",
                Imagen = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==",
                Nombre = "camp01",
                Prioridad = 1
            },
            new Campana
            {
                Activo = true,
                ActualizadoPor = "admin",
                Banner = @"<span><i class=""icon1-bag""></i></span>
                                <div class=""section__body-estilo1"">
                                    <ul>
                                        <li class=""list_first""><small>Dom 17 Jul</small> </li>
                                        <li>
                                            <div class=""label__titulo1"">
                                                <small><b>Shopping day<br />
                                                    Diners Club</b></small>
                                            </div>
                                        </li>
                                    </ul>
                                </div>",
                CreadoPor = "admin",
                Descripcion = "campana02",
                FechaActualizacion = DateTime.Today,
                FechaCreacion = DateTime.Today,
                FechaFin = DateTime.Parse("2016-12-31 00:00:00.000"),
                FechaInicio = DateTime.Parse("2016-01-01 00:00:00.000"),
                Id = "f0f75be5-3fe9-467b-9473-3c012014786f",
                Imagen = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==",
                Nombre = "camp02",
                Prioridad = 2
            },
            new Campana
            {
                Activo = true,
                ActualizadoPor = "admin",
                Banner = @"<span><i class=""icon1-bag""></i></span>
                                <div class=""section__body-estilo1"">
                                    <ul>
                                        <li class=""list_first""><small>Lun 18 Jul</small> </li>
                                        <li>
                                            <div class=""label__titulo1"">
                                                <small><b>Shopping day<br />
                                                    Diners Club</b></small>
                                            </div>
                                        </li>
                                    </ul>
                                </div>",
                CreadoPor = "admin",
                Descripcion = "campana03",
                FechaActualizacion = DateTime.Today,
                FechaCreacion = DateTime.Today,
                FechaFin = DateTime.Parse("2016-12-31 00:00:00.000"),
                FechaInicio = DateTime.Parse("2016-01-01 00:00:00.000"),
                Id = "f699fc38-61b9-4b75-afaa-419dab2c4d52",
                Imagen = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==",
                Nombre = "camp03",
                Prioridad = 3
            });
        }
    }
}
