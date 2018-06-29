﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface IServicioRepository
    {
        Task<List<Servicio>> ListarPorEmpresa(string idEmpresa);
    }
}
