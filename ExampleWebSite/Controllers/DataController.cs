using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExampleWebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public async Task<ActionResult<string>> Get([FromQuery] string machineName = "")
        {
            if (string.IsNullOrWhiteSpace(machineName))
            {
                return System.IO.File.ReadAllText("/Data/data.txt");
            }
            else
            {
                var client = new HttpClient();
                client.BaseAddress = new System.Uri("http://" + machineName);
                var data = await client.GetAsync("/api/Data");
                data.EnsureSuccessStatusCode();

                var content = await data.Content.ReadAsStringAsync();
                return content;
            }
        }
    }
}