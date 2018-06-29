using DinersClubOnline.Api.Filters;
using DinersClubOnline.Api.Models.Solicitudes;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace DinersClubOnline.Api.Controllers.Solicitudes
{
    [ScopeAuthorize(Scopes.Consultas)]
    [RoutePrefix("api/solicitudes/pdf")]
    public class SolicitudPdfController : ApiController
    {
        public SolicitudPdfController()
        {
        }

        [HttpPost]
        [Route]
        public HttpResponseMessage Post(SolicitudPdfViewModel datosSolicitud)
        {
            if (datosSolicitud == null) { return new HttpResponseMessage(HttpStatusCode.NotFound); }
            if (!datosSolicitud.Datos.Any()) { return new HttpResponseMessage(HttpStatusCode.OK); }

            var stream = new MemoryStream();
            stream = DinersClubOnline.Api.Infrastructure.Pdf.CrearPdfHelper.CrearPdfSolicitud(datosSolicitud);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(new MemoryStream(stream.GetBuffer()))
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/pdf"),
                        ContentDisposition = new ContentDispositionHeaderValue("inline")
                        {
                            FileName = "solicitud"
                        }
                    }
                }
            };
        }
    }
}