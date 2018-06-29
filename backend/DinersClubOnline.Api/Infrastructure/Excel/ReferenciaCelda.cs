using System;
using System.Text.RegularExpressions;

namespace DinersClubOnline.Api.Infrastructure.Excel
{
    /// <summary>Representa una referencia a una celda Excel con el formato {Columna}{Fila}</summary>
    [CLSCompliant(false)]
    public class ReferenciaCelda
    {
        private static readonly Regex ReferenceRegex = new Regex("^(?<columna>[A-Z]+)(?<fila>[0-9]+)$");
        private readonly string _columna;
        private readonly uint _fila;


        public string Columna
        {
            get { return _columna; }
        }

        public uint Fila
        {
            get { return _fila; }
        }

        public ReferenciaCelda(string columna, uint fila)
        {
            _columna = columna;
            _fila = fila;
        }

        public static bool TryParse(out ReferenciaCelda referenciaCelda, string stringCellReference)
        {
            referenciaCelda = null;

            if (stringCellReference == null) throw new ArgumentNullException("stringCellReference");

            var match = ReferenceRegex.Match(stringCellReference);

            if (!match.Success)
            {
                return false;
            }

            referenciaCelda = new ReferenciaCelda(match.Groups["columna"].Value, UInt32.Parse(match.Groups["fila"].Value));
            return true;
        }

        public override string ToString()
        {
            return String.Format("{0}{1}", Columna, Fila);
        }

        public ReferenciaCelda Arriba()
        {
            return new ReferenciaCelda(Columna, Fila - 1);
        }

        public ReferenciaCelda Abajo()
        {
            return new ReferenciaCelda(Columna, Fila + 1);
        }
    }
}