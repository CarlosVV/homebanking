using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DinersClubOnline.FakeRepositories
{
    public static class Data
    {
        private const decimal TipoDeCambio = 3.4m;

        public static readonly List<OperadorTelefonico> OperadoresTelefonicos = new List<OperadorTelefonico>();
        public static readonly List<Socio> Socios = new List<Socio>();
        public static readonly List<Usuario> Usuarios = new List<Usuario>();
        public static readonly List<Producto> Productos = new List<Producto>();
        public static readonly List<Banco> Bancos = new List<Banco>();
        public static readonly List<TipoPago> TiposPago = new List<TipoPago>();

        public static readonly List<Categoria> Categorias = new List<Categoria>();
        public static readonly List<Empresa> Empresas = new List<Empresa>();
        public static readonly List<Servicio> Servicios = new List<Servicio>();
        public static readonly List<Oferta> Ofertas = new List<Oferta>();
        public static readonly List<TipoDocumento> TiposDocumentos = new List<TipoDocumento>();
        public static readonly List<BancoAfiliado> BancosAfiliados = new List<BancoAfiliado>();

        public static readonly List<Privilegio> Privilegios = new List<Privilegio>();

        public static readonly List<AlertaTarjeta> AlertasTarjeta = new List<AlertaTarjeta>();

        public static List<Tarjeta> TarjetasTitulares
        {
            get { return Socios.SelectMany(x => x.Tarjetas).ToList(); }
        }

        public static List<Alerta> Alertas = new List<Alerta>();

        static Data()
        {
            #region OperadoresTelefonicos

            var claro = new OperadorTelefonico
            {
                Id = "1",
                Nombre = "Claro"
            };
            var movistar = new OperadorTelefonico
            {
                Id = "2",
                Nombre = "Movistar"
            };
            var entel = new OperadorTelefonico
            {
                Id = "3",
                Nombre = "Entel"
            };
            var bitel = new OperadorTelefonico()
            {
                Id = "4",
                Nombre = "Bitel"
            };

            OperadoresTelefonicos.AddRange(new[] { claro, movistar, entel, bitel });

            #endregion OperadoresTelefonicos

            #region Productos

            var clubMilesBlack = new Producto
            {
                Id = "1",
                Nombre = "Club Miles Black",
                AcumulaMillas = true
            };

            var clubMilesClassic = new Producto
            {
                Id = "2",
                Nombre = "Club Miles Classic",
                AcumulaMillas = true
            };

            var hiraoka = new Producto
            {
                Id = "3",
                Nombre = "Hiraoka",
                AcumulaMillas = false
            };

            Productos.AddRange(new[] { clubMilesBlack, clubMilesClassic, hiraoka });

            #endregion Productos

            #region Socios

            var socioJaime = new Socio
            {
                Nombre = "Jaime",
                SegundoNombre = "Pier",
                ApellidoPaterno = "Zamora",
                ApellidoMaterno = "Vasquez",
                TieneClaveDigital = true,
                NumeroDocumentoIdentidad = "40302010",
                FechaNacimiento = new DateTime(1981, 1, 1),
                TipoDocumentoIdentidad = "DNI",
                Sexo = "Masculino",
                EstadoCivil = "Soltero",
                Procedencia = "Perú",
                Tarjetas = new List<Tarjeta>
                {
                    new Tarjeta
                    {
                        Id = "TARJ1234567890",
                        Alias = "My Blacky",
                        LineaCreditoSoles = 22250m,
                        LineaCreditoDolares = 8500m,
                        LineaDisponibleSoles = 16377.48m,
                        LineaDisponibleDolares = 4679.28m,
                        MillasDisponibles = 23000,
                        NumeroTarjeta = "12345678901478",
                        Cvv2 = "159",
                        Producto = clubMilesBlack,
                        MontoMinimoSoles = 362.75m,
                        MontoMinimoDolares = 10.64m,
                        MontoTotalSoles = 1362.25m,
                        MontoTotalDolares = 446.36m,
                        EstadoTarjeta = EstadoTarjeta.Activo,

#region Adicionales

                        Adicionales = new List<Tarjeta>{
                            new Tarjeta {
                                Id = "TARJ1234567891",
                                Alias = null,
                                LineaCreditoSoles = 0,
                                LineaCreditoDolares = 0,
                                LineaDisponibleSoles = 0,
                                LineaDisponibleDolares = 0,
                                MillasDisponibles = 0,
                                NumeroTarjeta = "10215474748585",
                                Producto = clubMilesBlack,
                                EstadoTarjeta = EstadoTarjeta.Bloqueado,
                                NumeroOperacion = 1,
                                FechaOperacion = DateTime.Now,
                                Adicionales = new List<Tarjeta>(),
                                Socio = new Socio{
                                    Nombre = "Eli",
                                    SegundoNombre = "",
                                    ApellidoPaterno = "Zamora",
                                    ApellidoMaterno = "Johansson",
                                    TieneClaveDigital = false,
                                    NumeroDocumentoIdentidad = "74852547",
                                },
                                Movimientos = new List<Movimiento>{
                                    new Movimiento
                                    {
                                        Fecha = new DateTime(2016, 02, 01),
                                        Descripcion = "Mensualidad UPC",
                                        Lugar = "Perú",
                                        Cuotas = 5,
                                        ImporteSoles = 1500,
                                        ImporteDolares = 0,
                                    },
                                    new Movimiento
                                    {
                                        Fecha = new DateTime(2016, 02, 01),
                                        Descripcion = "Pago Perfume Chanel",
                                        Lugar = "Chanel Store - USA, Central Park",
                                        Cuotas = 0,
                                        ImporteSoles = 0,
                                        ImporteDolares = 599,
                                    },
                                    new Movimiento
                                    {
                                        Fecha = new DateTime(2016, 02, 02),
                                        Descripcion = "Pago Perfume Paco Rabanne",
                                        Lugar = "Paco Rabanne Store - USA, Central Park",
                                        Cuotas = 2,
                                        ImporteSoles = 0,
                                        ImporteDolares = 890,
                                    },
                                }
                            },
                            new Tarjeta {
                                Id = "TARJ123859585",
                                Alias = null,
                                LineaCreditoSoles = 0,
                                LineaCreditoDolares = 0,
                                LineaDisponibleSoles = 0,
                                LineaDisponibleDolares = 0,
                                MillasDisponibles = 0,
                                NumeroTarjeta = "10215474740052",
                                Producto = clubMilesBlack,
                                EstadoTarjeta = EstadoTarjeta.Bloqueado,
                                NumeroOperacion = 2,
                                FechaOperacion = DateTime.Now,
                                Adicionales = new List<Tarjeta>(),
                                Socio = new Socio{
                                    Nombre = "Sebastan",
                                    SegundoNombre = "",
                                    ApellidoPaterno = "Zamora",
                                    ApellidoMaterno = "Johansson",
                                    TieneClaveDigital = false,
                                    NumeroDocumentoIdentidad = "85968596",
                                },
                                Movimientos = new List<Movimiento>{
                                    new Movimiento
                                    {
                                        Fecha = new DateTime(2016, 02, 01),
                                        Descripcion = "Pago consumo Punto Azul",
                                        Lugar = "Perú",
                                        Cuotas = 0,
                                        ImporteSoles = 359,
                                        ImporteDolares = 0,
                                    }
                                }
                            }
                        },

#endregion Adicionales
#region EstadoCuenta
                        EstadoDeCuenta = new EstadoDeCuenta
                        {
                            FechaInicioPeriodo = DateTime.Today.AddDays(-40 + 1),
                            FechaFinPeriodo = DateTime.Today.AddDays(-10 + 1),
                            FechaVencimiento = DateTime.Today.AddDays(1),
                            Vencido = false,
                            DeudaAnteriorSoles = 1562.25m + 1000m,
                            DeudaAnteriorDolares = 446.36m + (1000m / TipoDeCambio),
                            AbonosSoles = -400.50m,
                            AbonosDolares = -400.50m / TipoDeCambio,
                            ConsumosSoles = 150.42m,
                            ConsumosDolares = 150.42m / TipoDeCambio,
                            ComisionesSoles = 10.55m,
                            ComisionesDolares = 10.55m / TipoDeCambio,
                            DeudaTotalSoles = 2762.25m,
                            DeudaTotalDolares = 446.36m,
                            MillasSaldoAnterior = 23000 + 400 -1500,
                            MillasGanadas = 1500,
                            MillasVencidas = 400,
                            MillasDisponibles = 23000
                        },
                        EstadosCuentaDescargaPdf = new List<EstadoCuentaDescargaPdf>{
                                new EstadoCuentaDescargaPdf {
                                    UrlDescarga = "ultimoMovimiento.pdf",
                                    Periodo = new Periodo(DateTime.Today)
                                },
                                new EstadoCuentaDescargaPdf {
                                    UrlDescarga = "septiembre.pdf",
                                    Periodo = new Periodo(DateTime.Today.AddMonths(-1))
                                },
                                 new EstadoCuentaDescargaPdf {
                                    UrlDescarga = "agosto.pdf",
                                    Periodo = new Periodo(DateTime.Today.AddMonths(-2))
                                }
                            },
#endregion
#region Movimientos

                        Movimientos = new List<Movimiento>{
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 10, 21),
                                Descripcion = "Pago Camisas Zara Man",
                                Lugar = "Perú",
                                Cuotas = 2,
                                ImporteSoles = 0,
                                ImporteDolares = 125,
                                PendienteProcesamiento = true
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 10, 20),
                                Descripcion = "Kids",
                                Lugar = "Perú",
                                Cuotas = 5,
                                ImporteSoles = 1095.36m,
                                ImporteDolares = 0,
                                PendienteProcesamiento = true
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 10, 19),
                                Descripcion = "Pago Zapatillas Adidas",
                                Lugar = "Perú",
                                Cuotas = 0,
                                ImporteSoles = 0,
                                ImporteDolares = 79,
                                PendienteProcesamiento = false
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 10, 19),
                                Descripcion = "Kids - Promo 1",
                                Lugar = "Perú",
                                Cuotas = 2,
                                ImporteSoles = 0,
                                ImporteDolares = 25,
                                PendienteProcesamiento = true,
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 10, 18),
                                Descripcion = "Pago Perfume Chanel",
                                Lugar = "Perú",
                                Cuotas = 6,
                                ImporteSoles = 750,
                                ImporteDolares = 0,
                                PendienteProcesamiento = false
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 09, 03),
                                Descripcion = "Pago Perfume Rabanne",
                                Lugar = "Perú",
                                Cuotas = 0,
                                ImporteSoles = 0,
                                ImporteDolares = 195,
                                PendienteProcesamiento = true
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 09, 05),
                                Descripcion = "Pago restaurant Long Horn",
                                Lugar = "Perú",
                                Cuotas = 2,
                                ImporteSoles = 369,
                                ImporteDolares = 0,
                                PendienteProcesamiento = false
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 09, 25),
                                Descripcion = "Pago cineplanet",
                                Lugar = "Perú",
                                Cuotas = 0,
                                ImporteSoles = 125.89m,
                                ImporteDolares = 0,
                                PendienteProcesamiento = false
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 06, 11),
                                Descripcion = "Consumo Canadian Bar",
                                Lugar = "Perú",
                                Cuotas = 1,
                                ImporteSoles = 2590,
                                ImporteDolares = 0,
                                PendienteProcesamiento = true
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 06, 04),
                                Descripcion = "Tablet Samsung G75",
                                Lugar = "Perú",
                                Cuotas = 0,
                                ImporteSoles = 0,
                                ImporteDolares = 219,
                                PendienteProcesamiento = false
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 09, 28),
                                Descripcion = "Pago Camisas Zara Man, New Collection, NY - USA, Special Edition",
                                Lugar = "Perú",
                                Cuotas = 2,
                                ImporteSoles = 0,
                                ImporteDolares = 125,
                                PendienteProcesamiento = true
                            },
                        },
#endregion Movimientos                        
#region Alertas
                        Alertas = new AlertaTarjetaUsuario 
                        {
                            IdUsuario = "1",
                            IdTarjeta = "TARJ1234567890",
                            EmailSeleccionado = 2,
                            EmailAdicional = "",
                            TelefonoAdicional = "789874654",
                            AlertasActivas =  new List<AlertaTarjeta>
                            {
                                new AlertaTarjeta
                                {
                                    IdAlerta = "1",
                                    Celular1Activo = true,
                                    Celular2Activo = false,
                                    Email1Activo = true,
                                    Email2Activo = false,
                                    IdCondicionAdicional = "2"
                                },
                                new AlertaTarjeta
                                {
                                    IdAlerta = "2",
                                    Celular1Activo = true,
                                    Celular2Activo = true,
                                    Email1Activo = true,
                                    Email2Activo = true,
                                    IdCondicionAdicional = "3"
                                },
                                new AlertaTarjeta
                                {
                                    IdAlerta = "3",
                                    Celular1Activo = false,
                                    Celular2Activo = false,
                                    Email1Activo = false,
                                    Email2Activo = false,
                                    IdCondicionAdicional = "4"
                                }
                            }                             
                        }                    
#endregion

                     },  
                    new Tarjeta
                    {
                        Id = "6546756578",
                        Alias = null,
                        LineaCreditoSoles = 4550000m,
                        LineaCreditoDolares = 1300000m,
                        LineaDisponibleSoles = 823627m,
                        LineaDisponibleDolares = 235322m,
                        MillasDisponibles = 15000,
                        NumeroTarjeta = "20052140052547",
                        Producto = clubMilesClassic,
                        MontoMinimoSoles = 262.24m,
                        MontoMinimoDolares = 10.64m,
                        MontoTotalSoles = 1492.25m,
                        MontoTotalDolares = 446.36m,
                        EstadoTarjeta = EstadoTarjeta.Activo,

#region Adicionales

                        Adicionales = new List<Tarjeta>{
                            new Tarjeta{
                                Id = "TARJ1234567893",
                                Alias = null,
                                LineaCreditoSoles = 0,
                                LineaCreditoDolares = 0,
                                LineaDisponibleSoles = 0,
                                LineaDisponibleDolares = 0,
                                MillasDisponibles = 0,
                                NumeroTarjeta = "96859909596958",
                                EstadoTarjeta = EstadoTarjeta.Activo,
                                Producto = clubMilesBlack,
                                Adicionales = new List<Tarjeta>(),
                                Socio = new Socio{
                                    Nombre = "An",
                                    SegundoNombre = "Sahara",
                                    ApellidoPaterno = "Zamora",
                                    ApellidoMaterno = "Johansson",
                                    TieneClaveDigital = false,
                                    NumeroDocumentoIdentidad = "74852547"
                                },
                                Movimientos = new List<Movimiento>{
                                   new Movimiento
                                    {
                                        Fecha = new DateTime(2016, 09, 12),
                                        Descripcion = "iPhone 7 white",
                                        Lugar = "Perú",
                                        Cuotas = 5,
                                        ImporteSoles = 0,
                                        ImporteDolares = 899,
                                        PendienteProcesamiento = true
                                    },
                                    new Movimiento
                                    {
                                        Fecha = new DateTime(2016, 09, 12),
                                        Descripcion = "Billetera Dolce Gabbana",
                                        Lugar = "Perú",
                                        Cuotas = 0,
                                        ImporteSoles = 0,
                                        ImporteDolares = 209,
                                        PendienteProcesamiento = true
                                    },
                                    new Movimiento
                                    {
                                        Fecha = new DateTime(2016, 09, 22),
                                        Descripcion = "Cartera Michael Kors",
                                        Lugar = "Perú",
                                        Cuotas = 3,
                                        ImporteSoles = 0,
                                        ImporteDolares = 969,
                                        PendienteProcesamiento = false
                                    },
                                }
                            }
                        },

#endregion Adicionales
#region EstadoCuenta
                        EstadoDeCuenta = new EstadoDeCuenta
                        {
                            FechaInicioPeriodo = DateTime.Today.AddDays(-40),
                            FechaFinPeriodo = DateTime.Today.AddDays(-10),
                            FechaVencimiento = DateTime.Today.AddDays(0),
                            Vencido = false,
                            DeudaAnteriorSoles = 2862.25m + 1000m,
                            DeudaAnteriorDolares = 646.36m + (1000m / TipoDeCambio),
                            AbonosSoles = -800.50m,
                            AbonosDolares = -100.50m / TipoDeCambio,
                            ConsumosSoles = 150.42m,
                            ConsumosDolares = 150.42m / TipoDeCambio,
                            ComisionesSoles = 50.55m,
                            ComisionesDolares = 10.55m / TipoDeCambio,
                            DeudaTotalSoles = 3862.25m,
                            DeudaTotalDolares = 746.36m,
                            MillasSaldoAnterior = 15000 + 300 -1700,
                            MillasGanadas = 1700,
                            MillasVencidas = 300,
                            MillasDisponibles = 15000
                        },
                        EstadosCuentaDescargaPdf = new List<EstadoCuentaDescargaPdf>{
                                new EstadoCuentaDescargaPdf {
                                    UrlDescarga = "ultimoMovimiento.pdf",
                                    Periodo = new Periodo(DateTime.Today)
                                },
                                new EstadoCuentaDescargaPdf {
                                    UrlDescarga = "septiembre.pdf",
                                    Periodo = new Periodo(DateTime.Today.AddMonths(-1))
                                }
                            },
#endregion
#region Movimientos

                        Movimientos = new List<Movimiento>{
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 10, 20),
                                Descripcion = "Pago Taxi Uber",
                                Lugar = "Perú",
                                Cuotas = 0,
                                ImporteSoles = 45,
                                ImporteDolares = 0
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 10, 18),
                                Descripcion = "Pago Miraflores hotel",
                                Lugar = "Perú",
                                Cuotas = 2,
                                ImporteSoles = 0,
                                ImporteDolares = 322,
                                PendienteProcesamiento = true
                            },
                            new Movimiento {
                                Fecha = new DateTime(2016, 10, 15),
                                Descripcion = "Pago Celular Galaxy S7 - Black Edition",
                                Lugar = "Perú",
                                Cuotas = 12,
                                ImporteSoles = 0
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 09, 21),
                                Descripcion = "POLO Jeans",
                                Lugar = "Perú",
                                Cuotas = 5,
                                ImporteSoles = 150,
                                ImporteDolares = 0
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 07, 22),
                                Descripcion = "Consumo KFC",
                                Lugar = "Perú",
                                Cuotas = 0,
                                ImporteSoles = 89,
                                ImporteDolares = 0
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 07, 22),
                                Descripcion = "Camisa verano - Zara Man",
                                Lugar = "Perú",
                                Cuotas = 0,
                                ImporteSoles = 150,
                                ImporteDolares = 0
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 07, 23),
                                Descripcion = "Mocasines Zara Man",
                                Lugar = "Perú",
                                Cuotas = 0,
                                ImporteSoles = 267,
                                ImporteDolares = 0,
                                PendienteProcesamiento = true
                            }
                        },
#endregion Movimientos
#region Alertas
                        Alertas = new AlertaTarjetaUsuario 
                        {
                            IdUsuario = "1",
                            IdTarjeta = "TARJ1234567890",
                            EmailSeleccionado = 2,
                            EmailAdicional = "",
                            TelefonoAdicional = "789874654",
                            AlertasActivas =  new List<AlertaTarjeta>
                            {
                                new AlertaTarjeta
                                {
                                    IdAlerta = "4",
                                    Celular1Activo = true,
                                    Celular2Activo = false,
                                    Email1Activo = true,
                                    Email2Activo = true,
                                    IdCondicionAdicional = "2"
                                }
                            }                             
                        }                    
#endregion

                    },
                    new Tarjeta
                    {
                        Id = "45564564",
                        Alias = null,
                        LineaCreditoSoles = 28000,
                        LineaCreditoDolares = 8000,
                        LineaDisponibleSoles = 5500m,
                        LineaDisponibleDolares = 1571.43m,
                        MillasDisponibles = 0,
                        NumeroTarjeta = "40142418299985",
                        Producto = hiraoka,
                        MontoMinimoSoles = 56.85m,
                        MontoMinimoDolares = 10.60m,
                        MontoTotalSoles = 2062.25m,
                        MontoTotalDolares = 476.36m,
                        EstadoTarjeta = EstadoTarjeta.Activo,
                        Adicionales = new List<Tarjeta>(),
#region EstadoCuenta
                        EstadoDeCuenta = new EstadoDeCuenta
                        {
                            FechaInicioPeriodo = DateTime.Today.AddDays(-40),
                            FechaFinPeriodo = DateTime.Today.AddDays(-10),
                            FechaVencimiento = DateTime.Today.AddDays(0),
                            Vencido = false,
                            DeudaAnteriorSoles = 1062.25m + 1000m,
                            DeudaAnteriorDolares = 446.36m + (100m / TipoDeCambio),
                            AbonosSoles = -400.50m,
                            AbonosDolares = -400.50m / TipoDeCambio,
                            ConsumosSoles = 150.42m,
                            ConsumosDolares = 150.42m / TipoDeCambio,
                            ComisionesSoles = 10.55m,
                            ComisionesDolares = 10.55m / TipoDeCambio,
                            DeudaTotalSoles = 2062.25m,
                            DeudaTotalDolares = 476.36m,
                            MillasSaldoAnterior = 0,
                            MillasGanadas = 0,
                            MillasVencidas = 0,
                            MillasDisponibles = 0
                        },
                        EstadosCuentaDescargaPdf = new List<EstadoCuentaDescargaPdf>{
                                new EstadoCuentaDescargaPdf {
                                    UrlDescarga = "ultimoMovimiento.pdf",
                                    Periodo = new Periodo(DateTime.Today)
                                },
                                new EstadoCuentaDescargaPdf {
                                    UrlDescarga = "septiembre.pdf",
                                    Periodo = new Periodo(DateTime.Today.AddMonths(-1))
                                }
                            }
#endregion
                    },
                },
                Ofertas = new List<Oferta>() {
                    new Oferta {
                        IdTipoOferta = "1",
                        IdTarjeta = "TARJ1234567890",
                        MontoLineaNueva = 32250.00m,
                        FechaInicio = DateTime.Today.AddDays(-2),
                        FechaFin = DateTime.Today.AddDays(10),
                        MontoOferta = 30000m,
                        TEA = 12,
                        TCEA = 10
                    },
                     new Oferta {
                        IdTipoOferta = "2",
                        IdTarjeta = "TARJ1234567890",
                        MontoLineaNueva = 32250.00m,
                        FechaInicio = DateTime.Today.AddDays(-2),
                        FechaFin = DateTime.Today.AddDays(10),
                        MontoOferta = 28000m,
                        TEA = 12,
                        TCEA = 10
                     },
                    //new Oferta {
                    //    IdTipoOferta = "3",
                    //    IdTarjeta = "6546756578",
                    //    MontoLineaNueva = 30000m,
                    //    FechaInicio = DateTime.Today.AddDays(-2),
                    //    FechaFin = DateTime.Today.AddDays(10),
                    //    MontoOferta = 26000,
                    //    TEA = 12,
                    //    TCEA = 10
                    //},
                    //new Oferta {
                    //    IdTipoOferta = "4",
                    //    IdTarjeta = "TARJ1234567890",
                    //    MontoLineaNueva = 30000m,
                    //    FechaInicio = DateTime.Today.AddDays(-2),
                    //    FechaFin = DateTime.Today.AddDays(10),
                    //    MontoOferta = 25000,
                    //    TEA = 12,
                    //    TCEA = 10
                    //}
                }
            };

            var socioCarlos = new Socio
            {
                Nombre = "Carlos",
                SegundoNombre = "Alberto",
                ApellidoPaterno = "Muñoz",
                ApellidoMaterno = "Rodriguez",
                TieneClaveDigital = true,
                NumeroDocumentoIdentidad = "10203040",
                TipoDocumentoIdentidad = "DNI",
                Sexo = "Masculino",
                EstadoCivil = "Soltero",
                Procedencia = "Perú",
                FechaNacimiento = new DateTime(1982, 2, 2),
                Tarjetas = new List<Tarjeta>
                {
                    new Tarjeta
                    {
                        Id = "CARD01234567",
                        Alias = "Mi CLub Miles",
                        LineaCreditoSoles = 30250,
                        LineaCreditoDolares = 5000,
                        LineaDisponibleSoles = 16377.48m,
                        LineaDisponibleDolares = 2679.28m,
                        MillasDisponibles = 23000,
                        NumeroTarjeta = "98765432109876",
                        Cvv2 = "123",
                        Producto = clubMilesBlack,
                        MontoMinimoSoles = 506.85m,
                        MontoMinimoDolares = 60.60m,
                        MontoTotalSoles = 3662.25m,
                        MontoTotalDolares = 276.36m,
                        EstadoTarjeta = EstadoTarjeta.Activo,
                        Adicionales = new List<Tarjeta>(),
                        EstadoDeCuenta = new EstadoDeCuenta
                        {
                            FechaInicioPeriodo = DateTime.Today.AddDays(-35),
                            FechaFinPeriodo = DateTime.Today.AddDays(-5),
                            FechaVencimiento = DateTime.Today.AddDays(-1),
                            Vencido = false,
                            DeudaAnteriorSoles = 3062.25m + 500m,
                            DeudaAnteriorDolares = 166.36m + (100m / TipoDeCambio),
                            AbonosSoles = -100.50m,
                            AbonosDolares = -100.50m / TipoDeCambio,
                            ConsumosSoles = 300.42m,
                            ConsumosDolares = 50.42m / TipoDeCambio,
                            ComisionesSoles = 10.55m,
                            ComisionesDolares = 10.55m / TipoDeCambio,
                            DeudaTotalSoles = 3662.25m,
                            DeudaTotalDolares = 276.36m,
                            MillasSaldoAnterior = 0,
                            MillasGanadas = 0,
                            MillasVencidas = 0,
                            MillasDisponibles = 23000
                        },
                        EstadosCuentaDescargaPdf = new List<EstadoCuentaDescargaPdf>{
                                new EstadoCuentaDescargaPdf {
                                    UrlDescarga = "ultimoMovimiento.pdf",
                                    Periodo = new Periodo(DateTime.Today)
                                },
                                new EstadoCuentaDescargaPdf {
                                    UrlDescarga = "septiembre.pdf",
                                    Periodo = new Periodo(DateTime.Today.AddMonths(-1))
                                }
                        },

                        #region Movimientos

                        Movimientos = new List<Movimiento>{
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 10, 20),
                                Descripcion = "Pago Taxi Seguro",
                                Lugar = "Perú",
                                Cuotas = 0,
                                ImporteSoles = 68.50m,
                                ImporteDolares = 0,
                                PendienteProcesamiento = true
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 10, 18),
                                Descripcion = "Pago Delfines hotel",
                                Lugar = "Perú",
                                Cuotas = 2,
                                ImporteSoles = 0,
                                ImporteDolares = 122
                            },
                            new Movimiento {
                                Fecha = new DateTime(2016, 10, 15),
                                Descripcion = "Pago Celular Galaxy S7 - Black Edition",
                                Lugar = "Perú",
                                Cuotas = 12,
                                ImporteSoles = 1750
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 09, 21),
                                Descripcion = "POLO Jeans",
                                Lugar = "Perú",
                                Cuotas = 5,
                                ImporteSoles = 150,
                                ImporteDolares = 0
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 09, 22),
                                Descripcion = "Consumo KFC",
                                Lugar = "Perú",
                                Cuotas = 0,
                                ImporteSoles = 89,
                                ImporteDolares = 0
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 09, 22),
                                Descripcion = "Camisa verano - Zara Man",
                                Lugar = "Perú",
                                Cuotas = 0,
                                ImporteSoles = 150,
                                ImporteDolares = 0
                            },
                            new Movimiento
                            {
                                Fecha = new DateTime(2016, 09, 23),
                                Descripcion = "Mocasines Zara Man",
                                Lugar = "Perú",
                                Cuotas = 0,
                                ImporteSoles = 267,
                                ImporteDolares = 0,
                                PendienteProcesamiento = true
                            }
                        }
                        

#endregion Movimientos
                    }
                },
                Ofertas = new List<Oferta>() { 
                    new Oferta {
                        IdTipoOferta = "1",
                        IdTarjeta = "CARD01234567",
                        MontoLineaNueva = 40250.00m,
                        FechaInicio = DateTime.Today.AddDays(-2),
                        FechaFin = DateTime.Today.AddDays(10),
                        MontoOferta = 32250.50m,
                        TEA = 12,
                        TCEA = 10
                    },
                     new Oferta {
                        IdTipoOferta = "2",
                        IdTarjeta = "CARD01234567",
                        MontoLineaNueva = 40250.00m,
                        FechaInicio = DateTime.Today.AddDays(-2),
                        FechaFin = DateTime.Today.AddDays(10),
                        MontoOferta = 32250.50m,
                        TEA = 12,
                        TCEA = 10
                    }
                }
            };

            var socioGerman = new Socio
            {
                Nombre = "German",
                SegundoNombre = "",
                ApellidoPaterno = "Nina",
                ApellidoMaterno = "Huisa",
                TieneClaveDigital = true,
                NumeroDocumentoIdentidad = "12345678",
                TipoDocumentoIdentidad = "DNI",
                Sexo = "Masculino",
                EstadoCivil = "Soltero",
                Procedencia = "Perú",
                FechaNacimiento = new DateTime(1983, 3, 3),
                Tarjetas = new List<Tarjeta>
                {
                    new Tarjeta
                    {
                        Id = "524564156456",
                        Alias = "Mi CLub Miles",
                        LineaCreditoSoles = 12250,
                        LineaCreditoDolares = 3500,
                        LineaDisponibleSoles = 6377.48m,
                        LineaDisponibleDolares = 1679.28m,
                        MillasDisponibles = 23000,
                        NumeroTarjeta = "33626325708257",
                        EstadoTarjeta = EstadoTarjeta.Activo,
                        Cvv2 = "333",
                        Producto = clubMilesBlack,
                        Adicionales = new List<Tarjeta>()
                    }
                },
                Ofertas = new List<Oferta>() { 
                     new Oferta {
                        IdTipoOferta = "1",
                        IdTarjeta = "524564156456",
                        MontoLineaNueva = 18250m,
                        FechaInicio = DateTime.Today.AddDays(-2),
                        FechaFin = DateTime.Today.AddDays(10),
                        MontoOferta = 15250m,
                        TEA = 12,
                        TCEA = 10
                    }
                }
            };

            var socioDennis = new Socio
            {
                Nombre = "Dennis",
                SegundoNombre = "Javier",
                ApellidoPaterno = "Huallanca",
                ApellidoMaterno = "Soto",
                TieneClaveDigital = true,
                NumeroDocumentoIdentidad = "87654321",
                FechaNacimiento = new DateTime(1984, 4, 4),
                TipoDocumentoIdentidad = "DNI",
                Sexo = "Masculino",
                EstadoCivil = "Soltero",
                Procedencia = "Perú",
                Tarjetas = new List<Tarjeta>
                {
                    new Tarjeta
                    {
                        Id = "4164156464564",
                        Alias = "Mi CLub Miles",
                        LineaCreditoSoles = 122500,
                        LineaCreditoDolares = 35000,
                        LineaDisponibleSoles = 16377.48m,
                        LineaDisponibleDolares = 4679.28m,
                        MillasDisponibles = 23000,
                        NumeroTarjeta = "14125140215458",
                        EstadoTarjeta = EstadoTarjeta.Activo,
                        Cvv2 = "444",
                        Producto = clubMilesBlack,
                        Adicionales = new List<Tarjeta>()
                    }
                }
            };

            var socioJorge = new Socio
            {
                Nombre = "Jorge",
                SegundoNombre = "",
                ApellidoPaterno = "Torres",
                ApellidoMaterno = "Sopla",
                TieneClaveDigital = false,
                NumeroDocumentoIdentidad = "12365478",
                FechaNacimiento = new DateTime(1985, 5, 5),
                TipoDocumentoIdentidad = "DNI",
                Sexo = "Masculino",
                EstadoCivil = "Soltero",
                Procedencia = "Perú",
                Tarjetas = new List<Tarjeta>
                {
                    new Tarjeta
                    {
                        Id = "145789",
                        Alias = "Mi CLub Miles",
                        LineaCreditoSoles = 14257,
                        LineaCreditoDolares = 3354,
                        LineaDisponibleSoles = 10751.63m,
                        LineaDisponibleDolares = 1256.47m,
                        MillasDisponibles = 33000,
                        NumeroTarjeta = "43210987654321",
                        EstadoTarjeta = EstadoTarjeta.Activo,
                        Producto = clubMilesBlack,
                        Cvv2 = "123",
                        Adicionales = new List<Tarjeta>()
                    }
                },
                Ofertas = new List<Oferta>() { 
                     new Oferta {
                        IdTipoOferta = "1",
                        IdTarjeta = "145789",
                        MontoLineaNueva = 19257m,
                        FechaInicio = DateTime.Today.AddDays(-2),
                        FechaFin = DateTime.Today.AddDays(10),
                        MontoOferta = 18257,
                        TEA = 12,
                        TCEA = 10
                    }
                }
            };

            Socios.AddRange(new[] { socioJaime, socioCarlos, socioGerman, socioDennis, socioJorge });
            Socios.ForEach(socio => socio.Tarjetas.ForEach(t =>
            {
                t.Socio = socio;

                t.Movimientos.ForEach(m =>
                {
                    m.Tarjeta = t;
                });

                t.Adicionales.ForEach(ta =>
                {
                    ta.Movimientos.ForEach(ma =>
                    {
                        ma.Tarjeta = ta;
                    });
                });
            }));

            #endregion Socios

            #region Usuarios

            var usuarioJaime = new Usuario
            {
                Id = "1",
                Alias = "",
                OperadorTelefonico = claro,
                NumeroCelular = "968574585",
                IdImagen = "0",
                IdFacebook = "10210745215890609",

                EmailPrincipal = "juan.perez.diners@gmail.com",
                EmailAlternativo = "rosa.perez.diners@gmail.com",
                EmailSeleccionado = "1",
                ClaveDigital = "147963",

                Socio = socioJaime
            };

            var usuarioCarlos = new Usuario
            {
                Id = "2",
                Alias = "",

                EmailPrincipal = "cmunoz@test001.com",
                EmailAlternativo = "cmunoz012345@test001.com",
                EmailSeleccionado = "1",
                OperadorTelefonico = entel,
                NumeroCelular = "999985898",
                IdImagen = "1",
                IdFacebook = "",

                ClaveDigital = "ABC123",

                Socio = socioCarlos
            };

            var usuarioGerman = new Usuario
            {
                Id = "3",
                Alias = "",

                EmailPrincipal = "gnina@diners.com.pe",
                EmailAlternativo = "gnina_15@test001.com",
                EmailSeleccionado = "1",
                OperadorTelefonico = claro,
                NumeroCelular = "968500254",
                IdImagen = "4",
                IdFacebook = "",

                ClaveDigital = "852cba",

                Socio = socioGerman
            };

            var usuarioDennis = new Usuario
            {
                Id = "4",
                Alias = "",

                EmailPrincipal = "dhuallanca@test001.com",
                EmailAlternativo = "dhuallanca_peru@test001.com",
                EmailSeleccionado = "2",
                OperadorTelefonico = movistar,
                NumeroCelular = "968375482",
                IdImagen = "7",
                IdFacebook = "",

                ClaveDigital = "963abc",

                Socio = socioDennis
            };

            Usuarios.AddRange(new[] { usuarioJaime, usuarioCarlos, usuarioGerman, usuarioDennis });

            #endregion Usuarios

            #region Bancos

            var bcp = new Banco
            {
                Id = "1",
                Nombre = "Banco de Credito"
            };
            var bbva = new Banco
            {
                Id = "2",
                Nombre = "BBVA Continental"
            };
            var scotia = new Banco
            {
                Id = "3",
                Nombre = "Scotiabank"
            };

            Bancos.AddRange(new[] { bcp, bbva, scotia });

            #endregion Bancos

            #region Bancos Afiliados

            var bcpAfiliado = new BancoAfiliado
            {
                IdBanco = "1",
                NombreBanco = "Banco de Crédito BCP",
                PagoVentanilla = true,
                PagoInternet = true,
                PagoCargoEnCuenta = true,
                UrlArchivo = "http://www.dinersclub.com.pe/sites/default/files/Pago%20Diners%20-%20BCP_0.pdf"
            };

            var bbvaAfiliado = new BancoAfiliado
            {
                IdBanco = "2",
                NombreBanco = "BBVA Continental",
                PagoVentanilla = true,
                PagoInternet = true,
                PagoCargoEnCuenta = true,
                UrlArchivo = "http://www.dinersclub.com.pe/sites/default/files/Pago%20Diners%20-%20BBVA_0.pdf"
            };

            var scotiabankAfiliado = new BancoAfiliado
            {
                IdBanco = "3",
                NombreBanco = "Scotiabank",
                PagoVentanilla = true,
                PagoInternet = true,
                PagoCargoEnCuenta = true,
                UrlArchivo = "http://www.dinersclub.com.pe/sites/default/files/Pago%20Diners%20-%20scotiabank_0.pdf"
            };

            var financieroAfiliado = new BancoAfiliado
            {
                IdBanco = "4",
                NombreBanco = "Banco Financiero",
                PagoVentanilla = true,
                PagoInternet = true,
                PagoCargoEnCuenta = false,
                UrlArchivo = "http://www.dinersclub.com.pe/sites/default/files/Pago%20Diners%20-%20BF.pdf"
            };

            var interbankAfiliado = new BancoAfiliado
            {
                IdBanco = "5",
                NombreBanco = "Interbank",
                PagoVentanilla = true,
                PagoInternet = true,
                PagoCargoEnCuenta = false,
                UrlArchivo = "http://www.dinersclub.com.pe/sites/default/files/Pago%20Diners%20-%20Interbank_0.pdf"
            };

            var banBifAfiliado = new BancoAfiliado
            {
                IdBanco = "6",
                NombreBanco = "BanBif",
                PagoVentanilla = true,
                PagoInternet = true,
                PagoCargoEnCuenta = false,
                UrlArchivo = "http://www.dinersclub.com.pe/sites/default/files/Pago%20Diners%20-%20BanBif.pdf"
            };

            var comercioAfiliado = new BancoAfiliado
            {
                IdBanco = "7",
                NombreBanco = "Banco de Comercio",
                PagoVentanilla = true,
                PagoInternet = false,
                PagoCargoEnCuenta = false,
                UrlArchivo = ""
            };

            var dinersAfiliado = new BancoAfiliado
            {
                IdBanco = "8",
                NombreBanco = "Diners Club",
                PagoVentanilla = true,
                PagoInternet = false,
                PagoCargoEnCuenta = false,
                UrlArchivo = ""
            };

            BancosAfiliados.AddRange(new[] { bcpAfiliado, bbvaAfiliado, scotiabankAfiliado, financieroAfiliado, interbankAfiliado, banBifAfiliado, comercioAfiliado, dinersAfiliado });

            #endregion

            #region Tipos Documentos

            var dni = new TipoDocumento
            {
                IdTipoDocumento = "1",
                NombreTipoDocumento = "DNI"
            };
            var pasaporte = new TipoDocumento
            {
                IdTipoDocumento = "2",
                NombreTipoDocumento = "PASAPORTE"
            };
            var carnet = new TipoDocumento
            {
                IdTipoDocumento = "3",
                NombreTipoDocumento = "CARNET EXTRANJERIA"
            };

            TiposDocumentos.AddRange(new[] { dni, pasaporte, carnet });

            #endregion Bancos

            #region TiposPago

            var pagoMinimo = new TipoPago
            {
                Id = "1",
                Nombre = "Pago total del mes"
            };
            var pagoTotal = new TipoPago
            {
                Id = "2",
                Nombre = "Pago mínimo del mes"
            };

            TiposPago.AddRange(new[] { pagoMinimo, pagoTotal });

            #endregion TiposPago

            #region Categorias

            var categoriaAerolias = new Categoria
            {
                IdCategoria = "0001",
                NombreCategoria = "Aerolineas"
            };
            var categoriaTelefonia = new Categoria
            {
                IdCategoria = "0002",
                NombreCategoria = "Telefonia"
            };
            var categoriaServicios = new Categoria
            {
                IdCategoria = "0003",
                NombreCategoria = "ServiciosBasicos"
            };
            var categoriaTecnologia = new Categoria
            {
                IdCategoria = "0004",
                NombreCategoria = "Tecnologia"
            };

            Categorias.AddRange(new[] { categoriaAerolias, categoriaTelefonia, categoriaServicios, categoriaTecnologia });

            #endregion Categorias

            #region Empresas

            var empresaLan = new Empresa
            {
                IdEmpresa = "0001",
                NombreEmpresa = "Lan",
                IdCategoria = "0001"
            };
            var empresaEntel = new Empresa
            {
                IdEmpresa = "0002",
                NombreEmpresa = "Entel",
                IdCategoria = "0002"
            };

            var empresaEdelnor = new Empresa
            {
                IdEmpresa = "0003",
                NombreEmpresa = "Edelnor",
                IdCategoria = "0003"
            };

            var empresaGolds = new Empresa
            {
                IdEmpresa = "0004",
                NombreEmpresa = "Gold's Gym",
                IdCategoria = "0003"
            };

            Empresas.AddRange(new[] { empresaLan, empresaEntel, empresaEdelnor, empresaGolds });

            #endregion Empresas

            #region Servicios

            var servicioVuelos = new Servicio
            {
                IdServicio = "0001",
                NombreServicio = "Vuelos",
                 ParametroServicio ="Numero Vuelo",
                IdEmpresa = "0001"
            };
            var servicioPostPago = new Servicio
            {
                IdServicio = "0002",
                NombreServicio = "PostPago",
                ParametroServicio = "Numero Celular",
                IdEmpresa = "0002"
            };
            var servicioLuz = new Servicio
            {
                IdServicio = "0003",
                NombreServicio = "Luz",
                ParametroServicio = "Codigo Cliente",
                IdEmpresa = "0003"
            };
            var servicioMembresia = new Servicio
            {
                IdServicio = "0004",
                NombreServicio = "Membresia",
                ParametroServicio = "Codigo Cliente",
                IdEmpresa = "0004"
            };
            var servicioReservaVuelo = new Servicio
            {
                IdServicio = "0005",
                NombreServicio = "Reservas vuelos",
                ParametroServicio = "Codigo Reserva",
                IdEmpresa = "0001"
            };
            var servicioTrifasico = new Servicio
            {
                IdServicio = "0006",
                NombreServicio = "Trifasico",
                ParametroServicio = "Codigo Cliente",
                IdEmpresa = "0003"
            };

            Servicios.AddRange(new[] { servicioVuelos, servicioPostPago, servicioLuz, servicioMembresia, servicioReservaVuelo, servicioTrifasico });

            #endregion Servicios

            #region Privilegios



            var asakusa = new Privilegio
            {
                Coordenada = new Coordenada(-12.0864016, -76.9965218),
                Nombre = "Asakusa Sushi Bar",
                NumeroCuotasSinIntereses = 6
            };

            var caplina = new Privilegio
            {
                Coordenada = new Coordenada(-12.0943101, -77.0168258),
                Nombre = "Caplina",
                NumeroCuotasSinIntereses = 3
            };

            var francesco = new Privilegio
            {
                Coordenada = new Coordenada(-12.123588, -77.0763239),
                Nombre = "Francesco",
                NumeroCuotasSinIntereses = 6
            };

            var laCarreta = new Privilegio
            {
                Coordenada = new Coordenada(-12.094605, -77.0289413),
                Nombre = "La Carreta",
                NumeroCuotasSinIntereses = 12
            };

            var laDamaJuana = new Privilegio
            {
                Coordenada = new Coordenada(-12.1399356, -77.0198106),
                Nombre = "La Dama Juana",
                NumeroCuotasSinIntereses = 9
            };

            var laGloriaDelCampo = new Privilegio
            {
                Coordenada = new Coordenada(-12.209798, -76.8642368),
                Nombre = "La Gloria del Campo",
                NumeroCuotasSinIntereses = 6
            };

            var muraglia = new Privilegio
            {
                Coordenada = new Coordenada(-12.1115055, -77.050928),
                Nombre = "Muraglia",
                NumeroCuotasSinIntereses = 12
            };

            var puroPeru = new Privilegio
            {
                Coordenada = new Coordenada(-12.1398924, -77.0198491),
                Nombre = "Puro Perú",
                NumeroCuotasSinIntereses = 9
            };

            Privilegios.AddRange(new[] { asakusa, caplina, francesco, laCarreta, laDamaJuana, laGloriaDelCampo, muraglia, puroPeru });
            #endregion

            #region Alertas
            var alertaConsumoRealizado = new Alerta
            {
                IdAlerta = "1",
                Nombre = "Consumos realizados",
                Descripcion = "Este mensaje le informará los consumos que realice en los sistemas contado y/o cuotas (tanto en Soles como Dólares).",
                CondicionesAdicionales = new List<AlertaCondicionAdicional>(){
                new AlertaCondicionAdicional { IdCondicionAdicional= "1", Nombre= "0" },
                new AlertaCondicionAdicional { IdCondicionAdicional= "2", Nombre= "50" },
                new AlertaCondicionAdicional { IdCondicionAdicional= "3", Nombre= "100" },
                new AlertaCondicionAdicional { IdCondicionAdicional= "4", Nombre= "250" },
                new AlertaCondicionAdicional { IdCondicionAdicional= "5", Nombre= "500" },
                new AlertaCondicionAdicional { IdCondicionAdicional= "6", Nombre= "1000" }}
            };

            var alertaCargoAutomatico = new Alerta
            {
                IdAlerta = "2",
                Nombre = "Cargo Automático",
                Descripcion = "Este mensaje le informará los cargos efectuados a su tarjeta por cada servicio inscrito al Cargo Automático Diners.",
                CondicionesAdicionales = new List<AlertaCondicionAdicional>()
            };

            var alertaFechaPago = new Alerta
            {
                IdAlerta = "6",
                Nombre = "Fecha de pago",
                Descripcion = "",
                CondicionesAdicionales = new List<AlertaCondicionAdicional>(){
                new AlertaCondicionAdicional { IdCondicionAdicional= "1", Nombre= "1" },
                new AlertaCondicionAdicional { IdCondicionAdicional= "2", Nombre= "2" },
                new AlertaCondicionAdicional { IdCondicionAdicional= "3", Nombre= "3" }}
            };

            var alertaPago = new Alerta
            {
                IdAlerta = "3",
                Nombre = "Pago",
                Descripcion = "Este mensaje le informará cada vez que Diners Club reciba un pago efectuado a su tarjeta.",
                CondicionesAdicionales = new List<AlertaCondicionAdicional>()
            };

            var alertaPromociones = new Alerta
            {
                IdAlerta = "4",
                Nombre = "Promociones",
                Descripcion = "Este mensaje le informará sobre las nuevas promociones que Diners Club tenga para usted.",
                CondicionesAdicionales = new List<AlertaCondicionAdicional>()
            };

            var alertaLineaCreditoDisponible = new Alerta
            {
                IdAlerta = "5",
                Nombre = "Linea de crédito disponible",
                Descripcion = "Este mensaje le recordará el porcentaje disponible de su línea de crédito, cuando los consumos realizados en el mesa alcancen o superen el porcentaje elegido.  Indique cual será el porcentaje disponible de su línea de crédito para recibir el mensaje.",
                CondicionesAdicionales = new List<AlertaCondicionAdicional>()
                {
                    new AlertaCondicionAdicional { IdCondicionAdicional= "1", Nombre= "100%" },
                    new AlertaCondicionAdicional { IdCondicionAdicional= "2", Nombre= "90%" },
                    new AlertaCondicionAdicional { IdCondicionAdicional= "3", Nombre= "80%" },
                    new AlertaCondicionAdicional { IdCondicionAdicional= "4", Nombre= "70%" },
                    new AlertaCondicionAdicional{ IdCondicionAdicional= "5", Nombre= "60%" },
                    new AlertaCondicionAdicional{ IdCondicionAdicional= "6", Nombre= "50%" },
                    new AlertaCondicionAdicional{ IdCondicionAdicional= "7", Nombre= "40%" },
                    new AlertaCondicionAdicional{ IdCondicionAdicional= "8", Nombre= "30%" },
                    new AlertaCondicionAdicional{ IdCondicionAdicional= "9", Nombre= "20%" },
                    new AlertaCondicionAdicional{ IdCondicionAdicional= "10", Nombre= "10%" }
                }
            };

            var alertaFechaVencimientoPreventivo = new Alerta
            {
                IdAlerta = "7",
                Nombre = "Fecha de vencimiento preventivo",
                Descripcion = "Este mensaje le recordará el importe y la fecha del último día de su Estado de cuenta en Soles y Dólares. Indique con cuántos días de anticipación desea recibir el mensaje.",
                CondicionesAdicionales = new List<AlertaCondicionAdicional>()
            };

            Alertas.Add(alertaConsumoRealizado);
            Alertas.Add(alertaCargoAutomatico);
            Alertas.Add(alertaFechaPago);
            Alertas.Add(alertaPago);
            Alertas.Add(alertaPromociones);
            Alertas.Add(alertaLineaCreditoDisponible);
            Alertas.Add(alertaFechaVencimientoPreventivo);
            #endregion
        }
    }
}