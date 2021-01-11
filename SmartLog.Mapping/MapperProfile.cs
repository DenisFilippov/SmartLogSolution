using System.Linq;
using AutoMapper;
using SmartLog.Core;
using SmartLog.DAL.Entities;
using SmartLog.Domain.Dto;
using SmartLog.Domain.Interfaces;

namespace SmartLog.Mapping
{
  public class MapperProfile : Profile
  {
    public const string LOG_TYPE_REPOSITORY = "LogTypeRepository";
    public const string LOGS_ID = "logsId";

    public MapperProfile()
    {
      CreateMap<LogEntity, LogDto>()
        .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(x => x.Uid, opt => opt.MapFrom(src => src.Uid))
        .ForMember(x => x.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
        .ForMember(x => x.Message, opt => opt.MapFrom(src => src.Message))
        .ForMember(x => x.Parent, opt => opt.MapFrom(src => src.Parent))
        .ForMember(x => x.Type, opt => opt.MapFrom(src => src.Type))
        ;

      CreateMap<CustomAttributeEntity, CustomAttributeDto>()
        .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(x => x.LogsId, opt => opt.MapFrom(src => src.LogsId))
        .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value))
        ;

      CreateMap<LogTypeEntity, LogTypeDto>()
        .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
        ;

      CreateMap<SmartLogDto, LogDto>()
        .ForMember(x => x.Uid, opt => opt.MapFrom(src => src.UId))
        .ForMember(x => x.Message, opt => opt.MapFrom(src => src.Message))
        .ForMember(x => x.MethodName, opt => opt.MapFrom(src => src.MethodName))
        .ForMember(x => x.Type, opt 
          => opt
            .MapFrom((src, _, _, context) 
              => ((ILogTypeRepository)(context.Items[LOG_TYPE_REPOSITORY]))
              .GetAsync()
              .GetAwaiter()
              .GetResult()
              .First(r => r.Name.ToUpper() == src.Type.ToUpper()).Id))
        .ForMember(x => x.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
        ;

      CreateMap<SmartCustomAttributeDto, CustomAttributeDto>()
        .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value))
        .ForMember(x => x.LogsId, opt => opt.MapFrom((src, _, _, context) => (long)context.Items[LOGS_ID]))
        ;

      CreateMap<SelectEntity, SelectDto>()
        .ForMember(x => x.LogsId, opt => opt.MapFrom(src => src.LogsId))
        .ForMember(x => x.LogGuid, opt => opt.MapFrom(src => src.LogGuid))
        .ForMember(x => x.Parent, opt => opt.MapFrom(src => src.Parent))
        .ForMember(x => x.MethodName, opt => opt.MapFrom(src => src.MethodName))
        .ForMember(x => x.Type, opt => opt.MapFrom(src => src.Type))
        .ForMember(x => x.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
        .ForMember(x => x.Message, opt => opt.MapFrom(src => src.Message))
        .ForMember(x => x.CustomAttributesId, opt => opt.MapFrom(src => src.CustomAttributesId))
        .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value))
        ;

      CreateMap<SelectDto, SmartLogDto>()
        .ForMember(x => x.UId, opt => opt.MapFrom(src => src.LogGuid))
        .ForMember(x => x.MethodName, opt => opt.MapFrom(src => src.MethodName))
        .ForMember(x => x.Type, opt
          => opt
            .MapFrom((src, _, _, context)
              => ((ILogTypeRepository)(context.Items[LOG_TYPE_REPOSITORY]))
              .GetAsync()
              .GetAwaiter()
              .GetResult()
              .First(r => r.Id == src.Type).Name))
        .ForMember(x => x.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
        .ForMember(x => x.Message, opt => opt.MapFrom(src => src.Message))
        ;

      CreateMap<SelectDto, SmartCustomAttributeDto>()
        .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Value))
        ;
    }
  }
}