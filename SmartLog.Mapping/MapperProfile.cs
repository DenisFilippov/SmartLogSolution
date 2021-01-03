using AutoMapper;
using SmartLog.DAL.Entities;
using SmartLog.Domain.Dto;

namespace SmartLog.Mapping
{
  public class MapperProfile : Profile
  {
    public MapperProfile()
    {
      CreateMap<LogEntity, LogDto>()
        .ForMember("Id", opt => opt.MapFrom(src => src.Id))
        .ForMember("Uid", opt => opt.MapFrom(src => src.Uid))
        .ForMember("CreateDate", opt => opt.MapFrom(src => src.CreateDate))
        .ForMember("Message", opt => opt.MapFrom(src => src.Message))
        .ForMember("Parent", opt => opt.MapFrom(src => src.Parent))
        .ForMember("Type", opt => opt.MapFrom(src => src.Type))
        ;

      CreateMap<CustomAttributeEntity, CustomAttributeDto>()
        .ForMember("Id", opt => opt.MapFrom(src => src.Id))
        .ForMember("LogsId", opt => opt.MapFrom(src => src.LogsId))
        .ForMember("Name", opt => opt.MapFrom(src => src.Name))
        .ForMember("Value", opt => opt.MapFrom(src => src.Value))
        ;

      CreateMap<LogTypeEntity, LogTypeDto>()
        .ForMember("Id", opt => opt.MapFrom(src => src.Id))
        .ForMember("Name", opt => opt.MapFrom(src => src.Name))
        ;
    }
  }
}