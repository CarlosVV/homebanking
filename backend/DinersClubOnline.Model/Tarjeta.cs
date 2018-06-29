using System.Linq;
using System.Collections.Generic;
using System;

namespace DinersClubOnline.Model
{
    /// <summary>Representa a una tarjeta titular o adicional</summary>
    public class Tarjeta
    {
        public Tarjeta()
        {
            Adicionales = new List<Tarjeta>();
            Movimientos = new List<Movimiento>();
            EstadoDeCuenta = new EstadoDeCuenta();
        }

        public string Id { get; set; }

        //Datos de tarjeta
        public string Alias { get; set; }

        //Linea de Crédito
        public decimal LineaCreditoSoles { get; set; }
        public decimal LineaCreditoDolares { get; set; }
        public decimal LineaDisponibleSoles { get; set; }
        public decimal LineaDisponibleDolares { get; set; }

        public EstadoTarjeta EstadoTarjeta { get; set; }

        //Millas
        public int MillasDisponibles { get; set; }

        //Seguridad
        public string NumeroTarjeta { get; set; }
        public string Cvv2 { get; set; }

        //Relaciones
        public Socio Socio { get; set; }
        public Producto Producto { get; set; }
        public List<Tarjeta> Adicionales { get; set; }
        public List<Movimiento> Movimientos { get; set; }

        /// <summary>
        /// Trae todos los movimientos de la tarjeta titular mas los movimientos de las tarjetas adcionales
        /// </summary>
        public List<Movimiento> MovimientosTotales
        { 
            get
            {
                return Movimientos.Concat(Adicionales.SelectMany(x => x.Movimientos)).ToList();
            }
        }

        public bool TieneAdicionales
        {
            get
            {
                return Adicionales.Any();
            }
        }

        public EstadoDeCuenta EstadoDeCuenta { get; set; }
        public List<EstadoCuentaDescargaPdf> EstadosCuentaDescargaPdf { get; set; }

        //Montos
        public decimal MontoMinimoSoles { get; set; }
        public decimal MontoMinimoDolares { get; set; }
        public decimal MontoTotalSoles { get; set; }
        public decimal MontoTotalDolares { get; set; }

        public AlertaTarjetaUsuario Alertas { get; set; }

        // Bloqueo tarjeta
        public int NumeroOperacion { get; set; }
        public DateTime FechaOperacion { get; set; }
    }

    public enum EstadoTarjeta
    {
        Bloqueado = 0,
        Activo = 1      
    }
}