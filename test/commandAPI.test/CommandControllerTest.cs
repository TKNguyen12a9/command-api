using System;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using Xunit;
using command.api.Models;
using command.api.DTOs;
using command.api.Controllers;
using Microsoft.AspNetCore.Mvc;
using command.api.Data;
using command.api.Profiles;

namespace commandAPI.test
{
    public class CommandsControllerTest : IDisposable
    {
        Mock<ICommandAPIRepo> mockRepo;
        CommandsProfile profile;
        MapperConfiguration configuration;
        IMapper mapper;

        public CommandsControllerTest()
        {
            mockRepo = new Mock<ICommandAPIRepo>();
            profile = new CommandsProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            profile = null;
            configuration = null;
            mapper = null;
        }


        #region GetCommandItems
        [Fact]
        public void GetCommandItems_Check200HTTP()
        {
            // Arrange
            mockRepo.Setup(repo =>
                    repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            // act 
            var result = controller.GetAllCommands();

            // assert
            // OkObjectResult == 200 status 

            // 2. Check 200 HTTP response
            Assert.IsType<OkObjectResult>(result.Result);

        }


        // Check the correct object type retuned
        [Fact]
        public void GetCommandItems_CheckCorrectType()
        {
            mockRepo.Setup(repo =>
                    repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            // act 
            var result = controller.GetAllCommands();
            Assert.IsType<ActionResult<IEnumerable<CommandReadDTO>>>(result);
        }


        // Check single resourced retunred
        [Fact]
        void GetCommandItems_CheckSingleResource()
        {
            mockRepo.Setup(repo =>
                    repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            // act
            var result = controller.GetAllCommands();
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDTO>;
            // assert
            Assert.Single(commands);
        }

        #endregion


        #region GetCommandByID

        [Fact]
        public void GetCommandByID_Check404HTTP()
        {
            mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(mockRepo.Object, mapper);

            // act 
            var result = controller.GetCommandById(1);
            // assert 
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetCommandById_Check200HTTP()
        {
            mockRepo.Setup(repo =>
                repo.GetCommandById(1)).Returns(new Command
                {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.GetCommandById(1);
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }


        // If we changed our
        // Controller code to return a different type, this test would fail
        [Fact]
        public void GetCommandById_CheckCorrectType()
        {
            //Arrange
            mockRepo.Setup(repo =>
                repo.GetCommandById(1)).Returns(new Command
                {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.GetCommandById(1);
            //Assert
            Assert.IsType<ActionResult<CommandReadDTO>>(result);
        }
        #endregion


        #region CreateCommand

        [Fact]
        public void CreateCommand_CheckCorrectType()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });

            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.CreateCommand(new CommandCreateDTO { });
            //Assert
            Assert.IsType<ActionResult<CommandReadDTO>>(result);
        }

        [Fact]
        public void CreateCommand_Check201HTTP()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.CreateCommand(new CommandCreateDTO { });
            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        #endregion


        #region UpdateCommand

        [Fact]
        public void UpdateCommand_Return204NoContent()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });

            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.UpdateCommand(1, new CommandUpdateDto { });
            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCommand_Check404HTTP()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.UpdateCommand(0, new CommandUpdateDto { });
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion


        #region PartialCommandUpdate

        [Fact]
        public void PartialCommandUpdate_Return404HTTP()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(mockRepo.Object, mapper);

            //Act
            var result = controller.PartialCommandUpdate(0,
            new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<CommandUpdateDto>
            { });
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion


        #region DeleteCommand

        [Fact]
        public void DeleteCommand_Return204HTTP()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });

            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.DeleteCommand(1);
            //Assert
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public void DeleteCommand_Return404HTTP()
        {
            //Arrange
            mockRepo.Setup(repo =>
            repo.GetCommandById(0)).Returns(() => null);

            var controller = new CommandsController(mockRepo.Object, mapper);
            //Act
            var result = controller.DeleteCommand(0);
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        private List<Command> GetCommands(int num)
        {
            var commands = new List<Command>();
            if (num > 0)
            {
                commands.Add(
                    new Command
                    {
                        Id = 0,
                        HowTo = "How to generate a migration",
                        CommandLine = "dotnet ef migrations add <Name of Migration>",
                        Platform = ".NET Core EF"
                    }
                );
            }

            return commands;
        }
    }
}