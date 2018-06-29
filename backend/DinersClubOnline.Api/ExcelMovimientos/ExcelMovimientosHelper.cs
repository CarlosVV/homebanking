using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DinersClubOnline.Api.Infrastructure.Excel;
using DinersClubOnline.Model;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DinersClubOnline.Api.ExcelMovimientos
{
    public class ExcelMovimientosHelper
    {
        private const uint NumFilaDummy = 8;

        public static void ActualizarStreamExcelConMovimientos(Stream archivoExcel, IEnumerable<Movimiento> movimientos)
        {
            if (archivoExcel == null) throw new ArgumentNullException("archivoExcel");
            if (movimientos == null) throw new ArgumentNullException("movimientos");


            using (var document = SpreadsheetDocument.Open(archivoExcel, true))
            {
                var hoja1 = document.WorkbookPart.WorksheetParts.First();

                //Eliminar fila dummy
                ExcelHelper.EliminarFila(hoja1, NumFilaDummy);

                //Insertar nuevas filas
                var insertIndex = NumFilaDummy;
                foreach (var movimiento in movimientos)
                {
                    var nuevaFila = ExcelHelper.InsertarFila(hoja1, insertIndex);

                    // Formato: Altura personalizada
                    nuevaFila.Height = 20.25;
                    nuevaFila.CustomHeight = true;
                    nuevaFila.DyDescent = 0.2;

                    var columna = 'A';
                    var indice = 0;

                    nuevaFila.InsertAt(new Cell
                    {
                        CellReference = new ReferenciaCelda(columna++.ToString(), insertIndex).ToString(),
                        StyleIndex = 1 // Celda Vacìa
                    }, indice++);
                    nuevaFila.InsertAt(new Cell
                    {
                        CellValue = new CellValue(movimiento.Fecha.ToOADate().ToString(CultureInfo.InvariantCulture)),
                        CellReference = String.Format("{0}{1}", columna++, insertIndex),
                        StyleIndex = 25 // Fecha en negrita
                    }, indice++);
                    nuevaFila.InsertAt(new Cell
                    {
                        CellValue = new CellValue(movimiento.Tarjeta.Socio.NombreCompleto),
                        DataType = new EnumValue<CellValues>(CellValues.String),
                        CellReference = String.Format("{0}{1}", columna++, insertIndex),
                        StyleIndex = 16
                    }, indice++);
                    nuevaFila.InsertAt(new Cell
                    {
                        CellValue = new CellValue(movimiento.Descripcion),
                        DataType = new EnumValue<CellValues>(CellValues.String),
                        CellReference = String.Format("{0}{1}", columna++, insertIndex),
                        StyleIndex = 16
                    }, indice++);
                    nuevaFila.InsertAt(new Cell
                    {
                        CellValue = new CellValue(movimiento.Lugar),
                        DataType = new EnumValue<CellValues>(CellValues.String),
                        CellReference = String.Format("{0}{1}", columna++, insertIndex),
                        StyleIndex = 16
                    }, indice++);
                    nuevaFila.InsertAt(new Cell
                    {
                        CellValue = new CellValue(movimiento.Cuotas != 0 ? movimiento.Cuotas.ToString() : ""),
                        DataType = new EnumValue<CellValues>(CellValues.Number),
                        CellReference = String.Format("{0}{1}", columna++, insertIndex),
                        StyleIndex = 17
                    }, indice++);
                    nuevaFila.InsertAt(new Cell
                    {
                        CellValue = new CellValue(movimiento.ImporteDolares != 0 ? movimiento.ImporteDolares.ToString(CultureInfo.InvariantCulture) : ""),
                        DataType = new EnumValue<CellValues>(CellValues.Number),
                        CellReference = String.Format("{0}{1}", columna++, insertIndex),
                        StyleIndex = 18 // US$ {Monto}
                    }, indice++);
                    nuevaFila.InsertAt(new Cell
                    {
                        CellValue = new CellValue(movimiento.ImporteSoles != 0 ? movimiento.ImporteSoles.ToString(CultureInfo.InvariantCulture) : ""),
                        DataType = new EnumValue<CellValues>(CellValues.Number),
                        CellReference = String.Format("{0}{1}", columna, insertIndex),
                        StyleIndex = 19 // S/ {Monto}
                    }, indice);

                    insertIndex++;
                }

                //Mover la celda combinada
                var mergeCell =
                    hoja1.Worksheet.GetFirstChild<MergeCells>()
                        .ChildElements.Cast<MergeCell>()
                        .FirstOrDefault(x => x.Reference == "B12:H12");
                mergeCell.Reference = String.Format("{0}:{1}", new ReferenciaCelda("B", insertIndex + 3),
                    new ReferenciaCelda("H", insertIndex + 3));

                hoja1.Worksheet.Save();
            }
        }
    }
}