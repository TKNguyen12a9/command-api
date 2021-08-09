using System;
using System.Collections.Generic;
using command.api.Data;
using Microsoft.AspNetCore.Mvc;
using command.api.Models;


namespace command.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repo;
        public CommandsController(ICommandAPIRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repo.GetAllCommands();
            return Ok(commandItems); // return a HTTP 200 result and pass back our result set 
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var command = _repo.GetCommandById(id);
            if (command is null)
            {
                return NotFound();
            }

            return Ok(command);
        }
    }
}