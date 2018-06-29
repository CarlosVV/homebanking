using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DinersClubOnline.Jobs.Tasks.Solicitudes.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace DinersClubOnline.Jobs.Tasks.Solicitudes
{
    public class ReportHandler : IReportHandler
    {
        #region métodos públicos
        public byte[] BuildReport(IEnumerable<SolicitudEnvioModel> lstSolicitudes)
        {
            return ListToExcel<SolicitudEnvioModel>(lstSolicitudes);

        }

        #endregion
        #region métodos privados
        private byte[] ListToExcel<T>(IEnumerable<T> query)
        {
            using (var pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Solicitudes" + DateTime.Now.ToString("ddMMyyyy"));
                var t = typeof(T);
                var Headings = t.GetProperties();
                for (int i = 0; i < Headings.Count(); i++)
                {
                    ws.Cells[1, i + 1].Value = Headings[i].Name;
                }

                if (query.Count() > 0)
                {
                    ws.Cells["A2"].LoadFromCollection(query);
                }

                using (ExcelRange rng = ws.Cells["A1:BZ1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
                    rng.Style.Font.Color.SetColor(Color.White);
                }
                return pck.GetAsByteArray();
            }

        }

        #endregion

    }
}
