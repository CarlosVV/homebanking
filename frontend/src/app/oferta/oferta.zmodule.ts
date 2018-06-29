angular.module('appDinersClubOnline.oferta', [])
.controller('Oferta', ['tarjetasApi', 'ofertasApi', 'sessionFactory', 'configuracion', '$sce', '$location', '$routeParams', Oferta.OfertaController]);