using Microsoft.EntityFrameworkCore;

namespace DotNetCoreExample.Models
{
    public class ColourContext : DbContext
    {
        public DbSet<Colour> Colours { get; set; }

        public ColourContext(DbContextOptions<ColourContext> options) : base(options)
        {

        }
    }
}
