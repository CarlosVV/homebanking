using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories.Solicitudes.Tests
{
    [TestClass]
    public class CargoAutomaticoTests
    {
        [TestMethod]
        public void VerificarConexion()
        {
            using(var ctx = new SolicitudContext())
            {
                var data = (from r in ctx.CargosAutomaticos
                           select r).ToList();
                
                Assert.IsNotNull(data);
            }
        }
    }
}
