using BankData.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BankData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATMsController : ControllerBase
    {
        // GET api/values
        [HttpGet("{ATMID}")]
        public ActionResult<ATMViewModel> Get([FromRoute]string ATMID)
        {
            var json = System.IO.File.ReadAllText("Data/Yorkshire.atms.json");
            var atms = JsonConvert.DeserializeObject<Result>(json);
            var atm = atms.SearchResults.SingleOrDefault(a => a.ATMId == ATMID);

            if (atm == null)
                return NotFound();
            else
                return new ATMViewModel()
                {
                    ATMID = ATMID,
                    Latitude = atm.Latitude,
                    Longitude = atm.Longitude
                };

        }

    }
}
