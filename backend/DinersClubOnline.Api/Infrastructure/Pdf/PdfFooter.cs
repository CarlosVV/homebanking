using iTextSharp.text;
using iTextSharp.text.pdf;
using System;

namespace DinersClubOnline.Api.Infrastructure.Pdf
{
    //setting CLSCompliant attribute to true
    [CLSCompliant(false)]
    public class PdfFooter : PdfPageEventHelper
    {
        [CLSCompliant(false)]
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            Font arialSmall = FontFactory.GetFont("Arial", 7);
            // Writing Footer on Page
            PdfPTable tab = new PdfPTable(1);
            PdfPCell cell = new PdfPCell(new Phrase("Copyright © Diners Club Perú S.A. " + DateTime.Today.Year, arialSmall));
            cell.Border = 0;
            tab.TotalWidth = 300F;
            tab.AddCell(cell);
            tab.WriteSelectedRows(0, -1, 100, 30, writer.DirectContent);
        }
    }
}