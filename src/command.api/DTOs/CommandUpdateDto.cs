

using System.ComponentModel.DataAnnotations;

namespace command.api.DTOs
{
    public class CommandUpdateDto
    {
        [Required, MaxLength(200)]
        public string HowTo { get; set; }
        [Required]
        public string Platform { get; set; }
        [Required]
        public string CommandLine { get; set; }
    }
}