using System;
using System.Configuration;
using CommandLine;
using DinersClub.Jobs.Tasks;
using DinersClubOnline.Jobs.Tasks.Solicitudes;
using DinersClubOnline.Jobs.Tasks.Solicitudes.Interfaces;

namespace DinersClubOnline.Jobs
{
    class Program
    {
        class Options
        {
            [Option('t', "tipotarea", Required = true,
              HelpText = "Tipo de Tarea")]
            public string TipoTarea { get; set; }

            [Option('s', "tiposolicitud", Required = true,
              HelpText = "Tipo de Solicitud")]
            public string TipoSolicitud { get; set; }

            [Option('i', "fechainicio", DefaultValue = false,
              HelpText = "Fecha de Inicio")]
            public bool FechaInicio { get; set; }

            [Option('f', "fechafin", DefaultValue = false,
              HelpText = "Fecha de Fin")]
            public bool FechaFin { get; set; }

        }
        static void Main(string[] args)
        {
            var result = CommandLine.Parser.Default.ParseArguments<Options>(args);

            switch (result.Value.TipoTarea.ToUpper())
            {
                case "ENVIO":
                    #region Tarea de Solicitudes
                    using (var data = new DataHandler())
                    {
                        IEmailHandler email = new EmailHandler();
                        IReportHandler report = new ReportHandler();
                        IReportConfiguration config = new ReportConfiguration();
                        SearchCriteria filtercriteria = new SearchCriteria();

                        config.EmailFrom = ConfigurationManager.AppSettings["Envio.Solicitudes.From"];
                        config.EmailTo = ConfigurationManager.AppSettings["Envio.Solicitudes.To"];
                        config.EmailSubject = ConfigurationManager.AppSettings["Envio.Solicitudes.Subject"] + " " + DateTime.Now.ToShortDateString();
                        config.EmailBody = ConfigurationManager.AppSettings["Envio.Solicitudes.Body"];
                        config.CredentialUser = ConfigurationManager.AppSettings["Envio.Solicitudes.CredentialUser"];
                        config.CredentialPassword = ConfigurationManager.AppSettings["Envio.Solicitudes.CredentialPassword"];

                        var solicitudtask = new SolicitudTask(data, report, email, config, filtercriteria);
                        solicitudtask.Process();
                    }
                    #endregion
                    break;
                case "OTRO":
                    break;
            }
        }
    }
}
