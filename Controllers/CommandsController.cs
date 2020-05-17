using System;
using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/[controller]")] // BaseRoute: api/commands
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepository _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commands = _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var command = _repository.GetCommandById(id);

            if (command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto body)
        {
            Command command = _mapper.Map<Command>(body);
            _repository.CreateCommand(command);
            if (_repository.SaveChanges()) {
                CommandReadDto response = _mapper.Map<CommandReadDto>(command);

                return CreatedAtRoute(nameof(GetCommandById), new {Id = response.Id}, response);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto body)
        {
            Command commandFromRepo = _repository.GetCommandById(id);
            if (commandFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(body, commandFromRepo);

            _repository.UpdateCommand(commandFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> body)
        {
            Command commandFromRepo = _repository.GetCommandById(id);
            if (commandFromRepo == null)
            {
                return NotFound();
            }

            CommandUpdateDto commandToPatch = _mapper.Map<CommandUpdateDto>(commandFromRepo);
            body.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            commandToPatch.UpdatedAt = DateTime.Now;

            _mapper.Map(commandToPatch, commandFromRepo);

            _repository.UpdateCommand(commandFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            Command command = _repository.GetCommandById(id);
            if (command == null)
            {
                return NotFound();
            }
            _repository.DeleteCommand(command);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
