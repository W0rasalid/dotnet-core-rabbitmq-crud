using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SharedLibrary.Models.DTOs;
using SharedLibrary.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharedLibrary.Mapping
{
    public class SystemLogProfile : Profile
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
