using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DinersClubOnline.Api.Controllers
{
    [RoutePrefix("api/time-zone")]
    public class TimeZoneDbController : ApiController
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string key = System.Configuration.ConfigurationManager.AppSettings["TimeZoneDbApiKey"];

        [Route]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string latitud, string longitud)
        {
            HttpResponseMessage response = await client.GetAsync("http://api.timezonedb.com/v2/get-time-zone?format=json&by=position&lat=" + latitud + "&lng=" + longitud + "&key=" + key);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<object>();
                return Ok(data);
            }
            else { return BadRequest("And error ocurred"); }
        }
    }
}
