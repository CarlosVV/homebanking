using System;

namespace DinersClubOnline.Model
{
    public struct Coordenada
    {
        private const double RadioDeLaTierra = 6371;

        private readonly double _latitud;
        private readonly double _longitud;

        public double Latitud
        {
            get { return _latitud; }
        }

        public double Longitud
        {
            get { return _longitud; }
        }

        public Coordenada(double latitud, double longitud)
        {
            _latitud = latitud;
            _longitud = longitud;
        }

        public double Distancia(Coordenada otraCoordenada)
        {
            var latitudRadianes = (otraCoordenada.Latitud - Latitud) * (Math.PI / 180);
            var longitudRadianes = (otraCoordenada.Longitud - Longitud) * (Math.PI / 180);
            var a = Math.Sin(latitudRadianes / 2) * Math.Sin(latitudRadianes / 2) + Math.Cos(Latitud * (Math.PI / 180)) * Math.Cos(otraCoordenada.Latitud * (Math.PI / 180)) * Math.Sin(longitudRadianes / 2) * Math.Sin(longitudRadianes / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return RadioDeLaTierra * c;
        }
    }
}