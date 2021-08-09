using System;
using System.ComponentModel.DataAnnotations;

namespace command.api.Models
{
    public class Command
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string HowTo { get; set; }

        [Required]
        public string Platform { get; set; }

        [Required]
        public string CommandLine { get; set; }
    }
}