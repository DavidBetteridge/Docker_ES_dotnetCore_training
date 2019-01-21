Create a new API project

Show Values Controller

Create a ColourService and Inject it

Change it to use IColour service

Add a random colour service implementation

Create table and Colours table

Create Model
```c#
        [Key]
        [MaxLength(100)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
```

Create ColourContext
```c#
    public class ColourContext : DbContext
    {
        public DbSet<Colour> Colours { get; set; }

        public ColourContext(DbContextOptions<ColourContext> options) : base(options)
        {

        }
    }
```
Aims for
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var colourCode = _colourService.TheColour();
            var colour = await _context.Colours.SingleOrDefaultAsync(c => c.Code == colourCode);

            return new string[] { colour.Name };
        }

then add 
            if (colour == null) return NotFound();


## ActionResult
Explain action results

## Logging
Run as console app and show logs (keep refreshing the page)


## Config Files
Override for development (secrets etc)

## Custom Settings
Create Settings.Email

services.Configure<Settings.Email>(
    options => Configuration.GetSection("Email").Bind(options));

IOptions<Settings.Email> settings

_logger.LogInformation("Email {SendTo} {SubjectLine}", _settings.ToAddress, _settings.SubjectLine);


## Exercise
Implement the rest of the colours controller

## Show testing framework