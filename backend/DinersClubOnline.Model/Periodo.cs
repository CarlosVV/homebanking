using System;

namespace DinersClubOnline.Model
{
    public class Periodo
    {
        private readonly int _mes;
        private readonly int _anho;

        public Periodo(int mes, int anho)
        {
            if (mes < 1 || mes > 12)
                throw new ArgumentOutOfRangeException("mes", mes, "mes debe estar entre 1 y 12");

            //TODO: Establecer el año mínimo
            if (anho < 0)
                throw new ArgumentOutOfRangeException("anho", anho, "anho debe ser positivo");

            _mes = mes;
            _anho = anho;
        }

        public Periodo(DateTime datetime)
        {
            _mes = datetime.Month;
            _anho = datetime.Year;
        }

        public int Mes { get { return _mes; } }
        public int Anho { get { return _anho; } }
    }
}
