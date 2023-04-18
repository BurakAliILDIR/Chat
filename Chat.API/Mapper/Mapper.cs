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
            CreateMap<Meet, GetMeetDto>();
            CreateMap<Message, GetMessageDto>();
            //CreateMap<Message[], ICollection<GetMessageDto>>();
            CreateMap<AppUser, GetAllUserDto>();
        }
    }
}