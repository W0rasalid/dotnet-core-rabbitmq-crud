using AutoMapper;
using RabbitMQ_Producer.Models.DTOs;
using SharedLibrary;
using SharedLibrary.Models.Entities;


namespace RabbitMQ_Producer.Helper.Mappings
{
    public class SystemLogProfile: Profile
    {
        public SystemLogProfile() 
        {
            CreateMap<LogSystemEntity, SytemLogRespDto>();
                //.ForMember(dest => dest.LogDate, opt => opt.MapFrom(src => src.LogDate))
                //.ForMember(dest => dest.Request, opt => opt.MapFrom(src => src.Request))
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
