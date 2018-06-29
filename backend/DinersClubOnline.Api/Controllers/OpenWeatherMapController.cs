using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DinersClubOnline.Api.Controllers
{
    [RoutePrefix("api/open-weather-map")]
    public class OpenWeatherMapController : ApiController
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string key = System.Configuration.ConfigurationManager.AppSettings["OpenWeatherMapApiKey"];

        [Route]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string latitud, string longitud)
        {
            HttpResponseMessage response = await client.GetAsync("http://api.openweathermap.org/data/2.5/weather?units=metric&lat=" + latitud + "&lon=" + longitud + "&appid=" + key);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<object>();
                return Ok(data);
            }
            else { return BadRequest("And error ocurred"); }
        }
    }
}
