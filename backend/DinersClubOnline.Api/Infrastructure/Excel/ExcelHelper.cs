using System;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DinersClubOnline.Api.Infrastructure.Excel
{
    /// <summary>Helper para operaciones con archivos excel</summary>
    [CLSCompliant(false)]
    public static class ExcelHelper
    {

        public static void EliminarFila(WorksheetPart hoja, uint numFila)
        {
            var sheetData = hoja.Worksheet.GetFirstChild<SheetData>();

            var filaAEliminar = sheetData.Elements<Row>().FirstOrDefault(l => l.RowIndex == numFila);

            //Eliminar la fila si existe
            if (filaAEliminar != null)
            {
                filaAEliminar.Remove();
            }

            //Si existen filas debajo de la eliminada moverlas 1 posición hacia arriba junto con sus celdas
            foreach (var filaASubir in sheetData.Elements<Row>().Where(l => l.RowIndex > numFila))
            {
                //Subir fila
                filaASubir.RowIndex--;

                //Subir sus celdas
                foreach (var celdaASubir in filaASubir.ChildElements.Cast<Cell>())
                {
                    ReferenciaCelda referenciaCelda;

                    if (ReferenciaCelda.TryParse(out referenciaCelda, celdaASubir.CellReference))
                    {
                        celdaASubir.CellReference = referenciaCelda.Arriba().ToString();
                    }
                }
            }
        }

        public static Row InsertarFila(WorksheetPart hoja, uint numFila)
        {
            var sheetData = hoja.Worksheet.GetFirstChild<SheetData>();

            var filaAnterior = sheetData.Elements<Row>().LastOrDefault(l => l.RowIndex < numFila);

            //TODO: En caso que la filaAnterior no exista


            //Si existen filas en el lugar o debajo de la insertada moverlas 1 posición hacia abajo junto con sus celdas
            foreach (var filaABajar in sheetData.Elements<Row>().Where(l => l.RowIndex >= numFila))
            {
                //Subir fila
                filaABajar.RowIndex++;

                //Subir sus celdas
                foreach (var celdaABajar in filaABajar.ChildElements.Cast<Cell>())
                {
                    ReferenciaCelda referenciaCelda;

                    if (ReferenciaCelda.TryParse(out referenciaCelda, celdaABajar.CellReference))
                    {
                        celdaABajar.CellReference = referenciaCelda.Abajo().ToString();
                    }
                }
            }

            var nuevaFila = new Row { RowIndex = numFila };
            sheetData.InsertAfter(nuevaFila, filaAnterior);

            return nuevaFila;
        }
    }
}