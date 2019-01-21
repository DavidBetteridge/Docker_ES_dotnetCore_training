using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreExample.Models;
using DotNetCoreExample.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotNetCoreExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColoursController : ControllerBase
    {
        private readonly IColourService _colourService;
        private readonly ColourContext _context;
        private readonly ILogger<ColoursController> _logger;
        private readonly Email _settings;

        public ColoursController(IColourService colourService, ColourContext context, 
                                ILogger<ColoursController> logger, IOptions<Settings.Email> settings)
        {
            _colourService = colourService;
            _context = context;
            _logger = logger;
            _settings = settings.Value;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var colourCode = _colourService.TheColour();

            _logger.LogInformation("Email {SendTo} {SubjectLine}", _settings.ToAddress, _settings.SubjectLine);
            _logger.LogInformation("Colour {ColourCode}", colourCode);

            var colour = await _context.Colours.SingleOrDefaultAsync(c => c.Code == colourCode);
            if (colour == null) return NotFound();

            return new string[] { colour.Name };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
