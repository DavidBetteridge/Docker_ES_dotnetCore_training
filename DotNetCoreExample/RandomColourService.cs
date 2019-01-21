using System;

namespace DotNetCoreExample
{
    public class RandomColourService : IColourService
    {
        public string TheColour()
        {
            var colours = new[] { "PINK", "GREEN", "BLUE", "YELLOW" };
            var rnd = new Random();
            return colours[rnd.Next(colours.Length)];
        }
    }
}
