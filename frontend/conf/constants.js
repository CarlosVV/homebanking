'use strict';

const comunes = {
  'urlGoogleMapsGeocodeApi': 'https://maps.googleapis.com/maps/api/geocode/json',
  'peruCountryCode': 'PE',
  'mensajeErrorWebApi': 'Lo sentimos la aplicación Diners Club Online se detuvo.',
  'mensajeEstadoCtaNoDisponible' : 'No hay aún un estado de cuenta disponible.',
  'mensajeMovimientosDisponible' : 'No hay resultados de búsqueda.',
  'mensajeMovimientosNoFacturados' : 'No cuenta con movimientos no facturados.'
};

const base = {
 local: {
    configuracion: {
      'expirationTimeSession': 3600,
      'urlServicioApi': 'http://localhost/DinersClubOnline.Api/api',
      'urlAuthServicioApi': 'http://localhost/DinersClubOnline.Auth',
      'facebookAppId': '1262524303760880',
      'urlDescargarPdf': 'http://localhost/DescargaEstadoCuenta'
    }
  },

  dev: {
    configuracion: {
      'expirationTimeSession': 3600,
      'urlServicioApi': 'https://lim-diners:8004/api',
      'urlAuthServicioApi': 'https://lim-diners:8003/',
      'facebookAppId': '1261497677196876',
      'urlDescargarPdf': 'https://lim-diners:8001/DescargaEstadoCuenta'
    }
  },

  uat: {
    configuracion: {
      'expirationTimeSession': 300,
      'urlServicioApi': 'https://201.234.55.22:8204/api',
      'urlAuthServicioApi': 'https://201.234.55.22:8203/',
      'facebookAppId': '1307828202563823',
      'urlDescargarPdf': 'https://201.234.55.22:8201/DescargaEstadoCuenta'
    }
  },

  dist: {
    configuracion: {
      'expirationTimeSession': 300,
      'urlServicioApi': 'https://lim-diners:8104/api',
      'urlAuthServicioApi': 'https://lim-diners:8103',
      'facebookAppId': '1261498950530082',
      'urlDescargarPdf': 'https://lim-diners:8101/DescargaEstadoCuenta'
    }
  }
};

const constantes = {};

const environments = Object.keys(base);

environments.forEach(function(env) {
  Object.assign(base[env].configuracion, comunes);
  constantes[env] = base[env];
});

exports.constants = constantes;

exports.environments = environments;
