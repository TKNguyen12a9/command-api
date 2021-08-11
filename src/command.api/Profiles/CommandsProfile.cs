using AutoMapper;
using command.api.Models;
using command.api.DTOs;

namespace command.api.Profiles
{
    // class is inherits from AutoMapper.Profile
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // CreateMap: map source object(Command) to the target object(CommandReadDTO)
            CreateMap<Command, CommandReadDTO>();
            // source (CommandCreateDTO) to the target (Command)
            CreateMap<CommandCreateDTO, Command>();
            CreateMap<CommandUpdateDto, Command>();
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}