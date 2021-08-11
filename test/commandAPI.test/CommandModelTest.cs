using System;
using Xunit;
using command.api.Models;


namespace commandAPI.test
{
    public class CommandAPITest : IDisposable
    {
        Command testCommand;

        public CommandAPITest()
        {
            // Arrange: create test command and populate with initial value
            testCommand = new Command
            {
                HowTo = "Do something with Xunit",
                Platform = "Xunit",
                CommandLine = "dotnet test"
            };
        }

        // inherit the IDisposable interface for code clean up 
        public void Dispose()
        {
            testCommand = null;
        }

        [Fact]
        public void CanChangeHowTo()
        {
            testCommand.HowTo = "Execute Unit test";
            // Assert: change value of HowTo matches what we expected
            Assert.Equal("Execute Unit test", testCommand.HowTo);
        }

        [Fact]
        public void CanChangePlatform()
        {
            testCommand.Platform = "Xunit";
            // Assert: change value of Platform matches what we expected
            Assert.Equal("Xunit", testCommand.Platform);
        }

        [Fact]
        public void CanChangeCommandLine()
        {
            testCommand.CommandLine = "dotnet test";
            // Assert: change value of CommandLine matches what we expected
            Assert.Equal("dotnet test", testCommand.CommandLine);
        }

    }
}