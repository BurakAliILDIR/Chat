using AutoMapper;
using Chat.API.CQRS.Meet.GetMessage;
using Chat.API.CQRS.User.GetAll;
using Chat.API.Entities;

namespace Chat.API.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Message, GetMessageDto>();
            CreateMap<AppUser, GetAllUserDto>();
        }
    }
}