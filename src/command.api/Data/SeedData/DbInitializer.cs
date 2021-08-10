using System.Collections.Generic;
using System.Linq;
using command.api.Models;


namespace command.api.Data.SeedData
{
    public static class DbInitializer
    {
        public static void InitializeDb(CommandContext context)
        {
            context.Database.EnsureCreated();

            if (context.CommandItems.Any())
            {
                return;
            }

            var commandItems = new Command[]
            {
                new Command
                {
                    HowTo = "Apply migration to DB",
                    Platform = "Entity Framework Core CLI",
                    CommandLine = "dotnet ef datatbase update"
                },

                new Command
                {
                    HowTo = "Create an EF migration",
                    Platform = "Entity Framework Core CLI",
                    CommandLine = "dotnet ef migrations add"
                },

                new Command
                {
                    HowTo = "Remove an EF migration",
                    Platform = "Entity Framework Core CLI",
                    CommandLine = "dotnet ef migrations remove"
                }
            };

            context.CommandItems.AddRange(commandItems);
            context.SaveChanges();
        }
    }
}
