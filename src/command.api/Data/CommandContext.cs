using System.Collections.Generic;
using command.api.Models;
using Microsoft.EntityFrameworkCore;

namespace command.api.Data
{
    public class CommandContext : DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options) : base(options)
        {
        }

        public DbSet<Command> CommandItems { get; set; }
    }
}