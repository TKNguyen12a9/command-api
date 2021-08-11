using System.Collections.Generic;
using System.Linq;
using command.api.Models;


namespace command.api.DTOs
{
    public class CommandReadDTO
    {
        public int Id { get; set; }
        public string HowTo { get; set; }
        public string Platform { get; set; }
        public string CommandLine { get; set; }
    }
}