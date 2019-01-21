using System.ComponentModel.DataAnnotations;

namespace DotNetCoreExample.Models
{
    public class Colour
    {
        [Key]
        [MaxLength(100)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}
