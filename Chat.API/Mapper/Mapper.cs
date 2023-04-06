using AutoMapper;
using Chat.API.CQRS.Meet.GetMessage.Dto;
using Chat.API.Entities;

namespace Chat.API.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Message, MessageDto>();
        }
    }
}