using DinersClubOnline.Api.Models.Solicitudes;
using DinersClubOnline.Api.Properties;
using DinersClubOnline.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace DinersClubOnline.Api.Infrastructure.Pdf
{
    public class CrearPdfHelper
    {
        public static MemoryStream CrearPdfSolicitud(SolicitudPdfViewModel datosSolicitud)
        {
            BaseColor colorAzul = new BaseColor(23, 54, 93);
            Font arialBold = FontFactory.GetFont("Arial", 12, Font.BOLD, colorAzul);
            Font arialNormal = FontFactory.GetFont("Arial", 12, Font.NORMAL);
            Font arialAzulNormal = FontFactory.GetFont("Arial", 12, Font.NORMAL, colorAzul);

            var readPdf = new PdfReader(Resources.DinersSolicitud);
            MemoryStream output = new MemoryStream();

            PdfStamper scratchPdf = new PdfStamper(readPdf, output);

            PdfContentByte pbover = scratchPdf.GetOverContent(1);
            //add content to the page using ColumnText
            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(datosSolicitud.Socio, arialAzulNormal), 58, 690, 0);

            ColumnText.ShowTextAligned(pbover, Element.ALIGN_LEFT, new Phrase(string.Format("Adjuntamos el detalle de tu {0}", datosSolicitud.Titulo), arialAzulNormal), 58, 670, 0);

            var tablaPdf = new PdfPTable(2);

            tablaPdf.SetTotalWidth(new float[] { 180, 200 });
            tablaPdf.WidthPercentage = 70;
            tablaPdf.HorizontalAlignment = 0;
            //leave a gap before and after the table
            tablaPdf.SpacingBefore = 20f;
            tablaPdf.SpacingAfter = 20f;

            foreach (var arrDatos in datosSolicitud.Datos)
            {
                for (var column = 0; column < 2; column++)
                {
                    var tipoLetra = column == 0 ? arialBold : arialNormal;
                    var celda = new PdfPCell(new Phrase(arrDatos[column], tipoLetra));
                    // sin borde y espacio entre celdas
                    celda.BorderWidth = 0;
                    celda.PaddingTop = 7;
                    tablaPdf.AddCell(celda);
                }
            }

            tablaPdf.WriteSelectedRows(0, tablaPdf.Rows.Count, 55, 650, scratchPdf.GetOverContent(1));

            scratchPdf.Close();

            return output;
        }

        public static MemoryStream CrearPdfConMovimientos(List<Movimiento> movimientos, DateTime fechaInicio, DateTime fechaFin)
        {
            MemoryStream output = new MemoryStream();
            var documentoOut = new Document();
            PdfWriter writer = PdfWriter.GetInstance(documentoOut, output);
            documentoOut.Open();
            // page header
            documentoOut.Add(CabeceraMovimientos());

            Font arial = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Font arialSmallBold = FontFactory.GetFont("Arial", 7, Font.BOLD);
            Font arialSmall = FontFactory.GetFont("Arial", 7);
            documentoOut.Add(new Paragraph(string.Format("Movimientos Históricos ({0} - {1})", fechaInicio.ToShortDateString(), fechaFin.ToShortDateString()), arial));

            var tablaPdf = new PdfPTable(8);

            tablaPdf.SetTotalWidth(new float[] { 5, 30, 60, 90, 100, 30, 40, 40 });
            tablaPdf.WidthPercentage = 100;
            tablaPdf.HorizontalAlignment = 0;
            //leave a gap before and after the table
            tablaPdf.SpacingBefore = 10f;
            tablaPdf.SpacingAfter = 30f;

            #region cabeceraTabla

            var celdaCabecera = AgregarCeldaHeaderMovimientos(new Phrase("", arialSmallBold));
            tablaPdf.AddCell(celdaCabecera);

            celdaCabecera = AgregarCeldaHeaderMovimientos(new Phrase("Fecha", arialSmallBold));
            tablaPdf.AddCell(celdaCabecera);

            celdaCabecera = AgregarCeldaHeaderMovimientos(new Phrase("Usuario", arialSmallBold));
            tablaPdf.AddCell(celdaCabecera);

            celdaCabecera = AgregarCeldaHeaderMovimientos(new Phrase("Descripción", arialSmallBold));
            tablaPdf.AddCell(celdaCabecera);

            celdaCabecera = AgregarCeldaHeaderMovimientos(new Phrase("Lugar", arialSmallBold));
            tablaPdf.AddCell(celdaCabecera);

            celdaCabecera = AgregarCeldaHeaderMovimientos(new Phrase("Cuotas", arialSmallBold));
            celdaCabecera.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tablaPdf.AddCell(celdaCabecera);

            celdaCabecera = AgregarCeldaHeaderMovimientos(new Phrase("Importe US$", arialSmallBold));
            celdaCabecera.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            tablaPdf.AddCell(celdaCabecera);

            celdaCabecera = AgregarCeldaHeaderMovimientos(new Phrase("Importe S/", arialSmallBold));
            celdaCabecera.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;

            tablaPdf.AddCell(celdaCabecera);

            #endregion cabeceraTabla

            #region bodyTabla

            foreach (var movimiento in movimientos)
            {
                var pendienteProcesamiento = movimiento.PendienteProcesamiento == true ? "*" : "";
                var celda = AgregarCeldaBodyMovimientos(new Phrase(pendienteProcesamiento, arialSmall));
                tablaPdf.AddCell(celda);
                //fecha
                celda = AgregarCeldaBodyMovimientos(new Phrase(movimiento.Fecha.ToShortDateString(), arialSmall));
                tablaPdf.AddCell(celda);
                //socio
                var socio = movimiento.Tarjeta.Socio != null ? string.Format("{0} {1} {2}", movimiento.Tarjeta.Socio.Nombre, movimiento.Tarjeta.Socio.ApellidoPaterno, movimiento.Tarjeta.Socio.ApellidoMaterno) : string.Empty;
                celda = AgregarCeldaBodyMovimientos(new Phrase(socio, arialSmall));
                tablaPdf.AddCell(celda);
                //Descripcion
                celda = AgregarCeldaBodyMovimientos(new Phrase(movimiento.Descripcion, arialSmall));
                tablaPdf.AddCell(celda);
                //lugar
                celda = AgregarCeldaBodyMovimientos(new Phrase(movimiento.Lugar, arialSmall));
                tablaPdf.AddCell(celda);
                //cuotas
                var cuotas = movimiento.Cuotas > 0 ? movimiento.Cuotas.ToString() : string.Empty;
                celda = AgregarCeldaBodyMovimientos(new Phrase(cuotas, arialSmall));
                celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tablaPdf.AddCell(celda);
                //importe dolares
                var importeDolares = movimiento.ImporteDolares > 0m ? movimiento.ImporteDolares.ToString("0,0.00") : string.Empty;
                celda = AgregarCeldaBodyMovimientos(new Phrase(importeDolares, arialSmall));
                celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                tablaPdf.AddCell(celda);
                //importe soles
                var importeSoles = movimiento.ImporteSoles > 0m ? movimiento.ImporteSoles.ToString("0,0.00") : string.Empty;
                celda = AgregarCeldaBodyMovimientos(new Phrase(importeSoles, arialSmall));
                celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                tablaPdf.AddCell(celda);
            }

            #endregion bodyTabla

            documentoOut.Add(tablaPdf);
            documentoOut.Add(new Paragraph("* Pendiente por procesar", arialSmall));
            writer.PageEvent = new PdfFooter();
            documentoOut.Close();
            return output;
        }

        private static PdfPTable CabeceraMovimientos()
        {
            var imagenLogo = System.Drawing.Image.FromHbitmap(Resources.LogoDiners.GetHbitmap());
            Image logo = Image.GetInstance(imagenLogo, System.Drawing.Imaging.ImageFormat.Png);

            var tablaPdf = new PdfPTable(2);

            tablaPdf.SetTotalWidth(new float[] { 200, 250 });
            tablaPdf.WidthPercentage = 100;
            tablaPdf.HorizontalAlignment = 0;

            tablaPdf.SpacingBefore = 5f;
            tablaPdf.SpacingAfter = 10f;

            var celdaImagen = new PdfPCell(logo);
            celdaImagen.Rowspan = 3;
            celdaImagen.Border = 0;
            tablaPdf.AddCell(celdaImagen);

            BaseColor colorAzul = new BaseColor(0, 82, 159);
            Font arial = FontFactory.GetFont("Arial", 10, Font.BOLD, colorAzul);
            Font arialSmall = FontFactory.GetFont("Arial", 7, Font.NORMAL, colorAzul);

            var celda = new PdfPCell(new Phrase("Diners Club Perú S.A.", arial));
            celda.HorizontalAlignment = Element.ALIGN_RIGHT;
            celda.Border = 0;
            tablaPdf.AddCell(celda);

            celda = new PdfPCell(new Phrase("Oficina: Av. Canaval y Moreyra 535, San Isidro", arialSmall));
            celda.HorizontalAlignment = Element.ALIGN_RIGHT;
            celda.Border = 0;
            tablaPdf.AddCell(celda);

            celda = new PdfPCell(new Phrase("socios@dinersclub.com.pe Teléfono: 615-1111 Fax: 442-3353", arialSmall));
            celda.HorizontalAlignment = Element.ALIGN_RIGHT;
            celda.Border = 0;
            tablaPdf.AddCell(celda);

            return tablaPdf;
        }

        private static PdfPCell AgregarCeldaBodyMovimientos(Phrase textoFormateado)
        {
            var celda = new PdfPCell(textoFormateado);
            // celda.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            celda.FixedHeight = 20;
            celda.BorderWidth = 0;
            celda.BorderWidthBottom = 1;
            celda.BorderColorBottom = BaseColor.GRAY;
            return celda;
        }

        private static PdfPCell AgregarCeldaHeaderMovimientos(Phrase textoFormateado)
        {
            var celda = new PdfPCell(textoFormateado);
            celda.FixedHeight = 20;
            celda.BorderWidth = 0;
            celda.BorderWidthBottom = 1;
            return celda;
        }
    }
}