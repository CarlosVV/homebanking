using DinersClubOnline.Model.Campanas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DinersClubOnline.Repositories.Campanas
{
    [CLSCompliant(true)]
    public class CampanaContext :  DbContext
    {
        public CampanaContext()
            : base("DbCampanas")
        { }

        public DbSet<Campana> Campanas { get; set; }
    }
}
