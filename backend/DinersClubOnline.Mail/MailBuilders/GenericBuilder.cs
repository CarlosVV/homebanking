//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace DinersClubOnline.Mail.MailBuilders
//{
//    public static class GenericBuilder
//    {
//        //public static string BuilEmail(Dictionary<string, string> values)
//        //{
//        //    var content = new StringBuilder();
//        //    BuildBody(values, content);

//        //    return content.ToString();
//        //}

//        private static void BuildHeader() { }

//        /// <summary>
//        /// Crea el html a partir del template solicitud.html
//        /// </summary>
//        /// <param name="usuarioNombres">Sera reemplazado en {{usuario}} del html</param>
//        /// <param name="values">Se creara una tabla y se pondra en {{contenido}} del html</param>
//        /// <returns>HTML con styles y formato</returns>
//        public static string BuildEmail(string titulo, string usuarioNombres, Dictionary<string, string> values)
//        {
//            var html = GetEmailHTml();
//            var content = new StringBuilder();
//            BuildBody(values, content);

//            var contenido = content.ToString();
//            html = html.Replace("{{usuario}}", usuarioNombres);
//            html = html.Replace("{{contenido}}", contenido);

//            return html;
//        }

//        private static void BuildBody(Dictionary<string, string> values, StringBuilder content)
//        {
//            content.Append("<table width='100%'>");

//            foreach (var item in values)
//            {
//                content.Append("<tr>");

//                content.Append("<td class='table-inner-first'>" + item.Key + "</td>");
//                content.Append("<td class='table-inner'>" + item.Value + "</td>");

//                content.Append("</tr>");
//            }

//            content.Append("</table>");
//        }

//        public static string BuilEmail(string titulo, string usuarioNombres, List<List<string>> values)
//        {
//            var html = GetEmailHTml();
//            var content = new StringBuilder();
//            BuildBody(values, content);
//            var contenido = content.ToString();
//            html = html.Replace("{{usuario}}", usuarioNombres);
//            html = html.Replace("{{contenido}}", contenido);
//            return html;
//        }

//        private static void BuildBody(List<List<string>> values, StringBuilder content)
//        {
//            content.Append("<table width='100%'>");

//            foreach (var item in values)
//            {
//                content.Append("<tr>");
//                foreach (var subItem in item)
//                {
//                    content.Append("<td class='table-inner'>" + subItem + "</td>");
//                    //content.Append("<td>" + subItem + "</td>");
//                }

//                content.Append("</tr>");
//            }

//            content.Append("</table>");
//        }

//        private static string GetEmailHTml()
//        {
//            var appDomain = System.AppDomain.CurrentDomain;
//            var basePath = appDomain.RelativeSearchPath ?? appDomain.BaseDirectory;
//            var templateSolicitud = Path.Combine(basePath, "Templates", "emailOperacion.html");
//            var html = File.ReadAllText(templateSolicitud);
//            return html;
//        }
//    }
//}