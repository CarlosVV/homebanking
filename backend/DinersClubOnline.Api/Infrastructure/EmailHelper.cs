using DinersClubOnline.Api.Models;
using DinersClubOnline.Api.Models.Solicitudes;
using DinersClubOnline.Api.Properties;
using DinersClubOnline.Mail.EmailService;
using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace DinersClubOnline.Api.Infrastructure
{
    public static class EmailHelper
    {
        public static void PrestamoPersonal_ProcesarEnviarCorreo(PrestamoPersonalViewModel prestamoPersonal, Usuario usuario, string numeroSolicitud, string fechaRegistro)
        {
            string correoUsuario = (usuario.EmailSeleccionado == "1" ? usuario.EmailPrincipal : usuario.EmailAlternativo);
            EnviaroCorreoSocio(prestamoPersonal.DatosCorreo, usuario.Socio.NombreCompleto, correoUsuario, "Canal Web – OFERTA PRÉSTAMO PERSONAL", numeroSolicitud, fechaRegistro);

            PrestamoPersonal_EnviaroCorreoDiners(prestamoPersonal, usuario, numeroSolicitud, fechaRegistro);
        }

        public static void DineroEfectivo_ProcesarEnviarCorreo(DineroEfectivoViewModel dineroEfectivo, Usuario usuario, string numeroSolicitud, string fechaRegistro)
        {
            string correoUsuario = (usuario.EmailSeleccionado == "1" ? usuario.EmailPrincipal : usuario.EmailAlternativo);
            EnviaroCorreoSocio(dineroEfectivo.DatosCorreo, usuario.Socio.NombreCompleto, correoUsuario, "Canal Web – OFERTA DINERO EN EFECTIVO", numeroSolicitud, fechaRegistro);
            DineroEfectivo_EnviaroCorreoDiners(dineroEfectivo, usuario, numeroSolicitud, fechaRegistro);
        }

        public static void TarjetaAdicional_ProcesarEnviarCorreo(TarjetaAdicionalViewModel tarjetaAdicional, Usuario usuario, string numeroSolicitud, string fechaRegistro)
        {
            string correoUsuario = (usuario.EmailSeleccionado == "1" ? usuario.EmailPrincipal : usuario.EmailAlternativo);
            EnviaroCorreoSocio(tarjetaAdicional.DatosCorreo, usuario.Socio.NombreCompleto, correoUsuario, "Canal Web – SOLICITUD TARJETA ADICIONAL", numeroSolicitud, fechaRegistro);
            TarjetaAdicional_EnviaroCorreoDiners(tarjetaAdicional, usuario, numeroSolicitud, fechaRegistro);
        }

        public static void BloqueoTarjeta_ProcesarEnviarCorreo(BloquearTarjetaViewModel tarjeta, Usuario usuario, string numeroOperacion, DateTime fechaOperacion)
        {
            string correoUsuario = (usuario.EmailSeleccionado == "1" ? usuario.EmailPrincipal : usuario.EmailAlternativo);
            var datosCorreo = BloqueoTarjeta_ConstruirContenido(tarjeta, numeroOperacion, fechaOperacion);
            var nombreCompleto = tarjeta.NombreTarjetaHabiente + " " + tarjeta.ApellidoPaternoTarjetaHabiente;

            EnviaroCorreoSocio(datosCorreo, nombreCompleto, correoUsuario, "Canal Web – BLOQUEO TARJETA", numeroOperacion, fechaOperacion.ToString("dd/MM/yyyy HH:mm"));
            BloqueoTarjeta_EnviaroCorreoDiners(datosCorreo, nombreCompleto, numeroOperacion, fechaOperacion.ToString("dd/MM/yyyy HH:mm"));
        }

        private static void EnviaroCorreoSocio(List<Diccionario> datos, string nombreCompleto, string correoUsuario, string asunto, string numeroSolicitud, string fechaRegistro)
        {
            var mailsTo = new List<string> { correoUsuario };

            datos.Insert(0, new Diccionario { Key = "Fecha y hora", Value = fechaRegistro });
            datos.Insert(1, new Diccionario { Key = "Nº Solicitud", Value = numeroSolicitud });
            var contenidoCorreo = CrearHtmlOperacionEmail(nombreCompleto, datos);

            EmailSenderService.SendEmail(asunto, contenidoCorreo, "diners.email.test@gmail.com", mailsTo, null, null, null);
        }

        private static void PrestamoPersonal_EnviaroCorreoDiners(PrestamoPersonalViewModel prestamoPersonal, Usuario usuario, string numeroSolicitud, string fechaRegistro)
        {
            var correoDinersSac = ConfigurationManager.AppSettings["CorreoDinersSac"];
            var mailsTo = new List<string> { correoDinersSac };
            var contenidoCorreo = CrearHtmlOperacionEmail(usuario.Socio.NombreCompleto, prestamoPersonal.DatosCorreo);

            EmailSenderService.SendEmail("Canal Web – OFERTA PRÉSTAMO PERSONAL", contenidoCorreo, "diners.email.test@gmail.com", mailsTo, null, null, null);
        }

        private static void DineroEfectivo_EnviaroCorreoDiners(DineroEfectivoViewModel dineroEfectivo, Usuario usuario, string numeroSolicitud, string fechaRegistro)
        {
            var correoDinersSac = ConfigurationManager.AppSettings["CorreoDinersSac"];
            var mailsTo = new List<string> { correoDinersSac };
            var contenidoCorreo = CrearHtmlOperacionEmail(usuario.Socio.NombreCompleto, dineroEfectivo.DatosCorreo);

            EmailSenderService.SendEmail("Canal Web – OFERTA DINERO EN EFECTIVO", contenidoCorreo, "diners.email.test@gmail.com", mailsTo, null, null, null);
        }

        private static void TarjetaAdicional_EnviaroCorreoDiners(TarjetaAdicionalViewModel tarjetaAdicional, Usuario usuario, string numeroSolicitud, string fechaRegistro)
        {
            var correoDinersSac = ConfigurationManager.AppSettings["CorreoDinersSac"];
            var mailsTo = new List<string> { correoDinersSac };
            var contenidoCorreo = CrearHtmlOperacionEmail(usuario.Socio.NombreCompleto, tarjetaAdicional.DatosCorreo);

            EmailSenderService.SendEmail("Canal Web – SOLICITUD TARJETA ADICIONAL", contenidoCorreo, "diners.email.test@gmail.com", mailsTo, null, null, null);
        }
        
        public static void Movimientos_ProcesarEnviarCorreo(string correoDestino, string asunto, Dictionary<MemoryStream, string> attachments)
        {
            var mailsTo = new List<string> { correoDestino };
            var contenidoCorreo = "";

            EmailSenderService.SendEmail(asunto, contenidoCorreo, "diners.email.test@gmail.com", mailsTo, null, null, attachments);
        }

        private static void BloqueoTarjeta_EnviaroCorreoDiners(List<Diccionario> datosCorreo, string nombreCompleto, string numeroSolicitud, string fechaRegistro)
        {
            var correoDinersSac = ConfigurationManager.AppSettings["CorreoDinersSac"];
            var mailsTo = new List<string> { correoDinersSac };
            var contenidoCorreo = CrearHtmlOperacionEmail(nombreCompleto, datosCorreo);

            EmailSenderService.SendEmail("Canal Web – BLOQUEO TARJETA", contenidoCorreo, "diners.email.test@gmail.com", mailsTo, null, null, null);
        }

        private static List<Diccionario> BloqueoTarjeta_ConstruirContenido(BloquearTarjetaViewModel model, string numeroOperacion, DateTime fechaOperacion) 
        {
            var datosCorreo = new List<Diccionario>();
            //datosCorreo.Add(new Diccionario { Key = "N° Operación", Value = numeroOperacion });
            //datosCorreo.Add(new Diccionario { Key = "Fecha y hora", Value = fechaOperacion.ToString("dd/MM/yyyy HH:mm") });
            datosCorreo.Add(new Diccionario { Key = "Tarjeta", Value = model.FormatoNombreTarjeta });
            datosCorreo.Add(new Diccionario { Key = (model.EsTitular ? "Titular" : "Adicional"), Value = model.NombreTarjetaHabiente + " " + model.ApellidoPaternoTarjetaHabiente });
            datosCorreo.Add(new Diccionario { Key = "Motivo", Value = (model.IdMotivo == "robo" ? "Robo" : "Pérdida") });

            return datosCorreo;
        }

        #region Crear html Correo
        /// <summary>
        /// Crea el html a partir del template solicitud.html
        /// </summary>
        /// <param name="usuarioNombres">Sera reemplazado en {{usuario}} del html</param>
        /// <param name="values">Se creara una tabla y se pondra en {{contenido}} del html</param>
        /// <returns>HTML con styles y formato</returns>
        public static string CrearHtmlOperacionEmail(string usuarioNombres, List<Diccionario> values)
        {
            var titulo = "COMPROBANTE DE OPERACIÓN";
            var html = GetEmailHTml();
            var content = new StringBuilder();
            CrearHtmlBody(values, content);

            var contenido = content.ToString();
            html = html.Replace("{{titulo}}", titulo);
            html = html.Replace("{{usuario}}", usuarioNombres);
            html = html.Replace("{{contenido}}", contenido);

            return html;
        }

        /// <summary>
        /// Crea el html a partir del template solicitud.html
        /// </summary>
        /// <param name="titulo">sera reemplazado en {{titulo}}</param>
        /// <param name="usuarioNombres">Sera reemplazado en {{usuario}} del html</param>
        /// <param name="values">Se creara una tabla y se pondra en {{contenido}} del html</param>
        /// <returns>HTML con styles y formato</returns>
        public static string CrearHtmlOperacionEmail(string titulo, string usuarioNombres, List<Diccionario> values)
        {
            titulo = "COMPROBANTE DE OPERACIÓN";
            var html = GetEmailHTml();
            var content = new StringBuilder();
            CrearHtmlBody(values, content);

            var contenido = content.ToString();
            html = html.Replace("{{titulo}}", titulo);
            html = html.Replace("{{usuario}}", usuarioNombres);
            html = html.Replace("{{contenido}}", contenido);

            return html;
        }

        public static string CrearHtmlOperacionEmail(string usuarioNombres, List<List<string>> values)
        {
            var titulo = "COMPROBANTE DE OPERACIÓN";
            var html = GetEmailHTml();
            var content = new StringBuilder();
            CrearHtmlBody(values, content);
            var contenido = content.ToString();
            html = html.Replace("{{titulo}}", titulo);
            html = html.Replace("{{usuario}}", usuarioNombres);
            html = html.Replace("{{contenido}}", contenido);
            return html;
        }

        public static string CrearHtmlOperacionEmail(string titulo, string usuarioNombres, List<List<string>> values)
        {
            var html = GetEmailHTml();
            var content = new StringBuilder();
            CrearHtmlBody(values, content);
            var contenido = content.ToString();
            html = html.Replace("{{titulo}}", titulo);
            html = html.Replace("{{usuario}}", usuarioNombres);
            html = html.Replace("{{contenido}}", contenido);
            return html;
        }

        private static void CrearHtmlBody(List<Diccionario> values, StringBuilder content)
        {
            content.Append("<table width='100%'>");

            foreach (var item in values)
            {
                content.Append("<tr>");

                content.Append("<td class='table-inner-first'>" + item.Key + "</td>");
                content.Append("<td class='table-inner'>" + item.Value + "</td>");

                content.Append("</tr>");
            }

            content.Append("</table>");
        }

        private static void CrearHtmlBody(List<List<string>> values, StringBuilder content)
        {
            content.Append("<table width='100%'>");

            foreach (var item in values)
            {
                content.Append("<tr>");
                foreach (var subItem in item)
                {
                    content.Append("<td class='table-inner'>" + subItem + "</td>");
                    //content.Append("<td>" + subItem + "</td>");
                }

                content.Append("</tr>");
            }

            content.Append("</table>");
        }

        private static string GetEmailHTml()
        {
            return Resources.EmailOperacion; ;
        }
        #endregion
    }
}