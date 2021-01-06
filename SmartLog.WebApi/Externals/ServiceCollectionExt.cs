using AutoMapper;
using Dapper.FluentMap;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartLog.DAL.Map;
using SmartLog.DAL.Repository;
using SmartLog.Domain;
using SmartLog.Domain.Dto;
using SmartLog.Domain.Interfaces;
using SmartLog.Mapping;
using SmartLog.Service;

namespace SmartLog.WebApi.Externals
{
  public static class ServiceCollectionExt
  {
    public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
    {
      services.AddSingleton<IConfigDto>(_ => new ConfigDto
      {
        Connection = new ConnectionDto
        {
          ConnectionString = config["ConnectionString"]
        }
      });

      return services;
    }

    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
      services.AddSingleton<IMapper>(_ =>
      {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperProfile()); });
        return new Mapper(config);
      });

      return services;
    }

    public static IServiceCollection AddFluentMapper(this IServiceCollection services)
    {
      FluentMapper.Initialize(c =>
      {
        c.AddMap(new LogEntityMap());
        c.AddMap(new CustomAttributeEntityMap());
        c.AddMap(new LogTypeEntityMap());
      });

      return services;
    }

    public static IServiceCollection AddConnector(this IServiceCollection services)
    {
      services.AddSingleton<IConnector>(r =>
      {
        var config = r.GetService<IConfigDto>();
        return new Connector(config.Connection.ConnectionString);
      });

      return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
      services.AddScoped<ILogRepository>(r =>
      {
        var mapper = r.GetService<IMapper>();
        return new LogRepository(mapper);
      });
      services.AddScoped<ICustomAttributeRepository>(r =>
      {
        var mapper = r.GetService<IMapper>();
        return new CustomAttributeRepository(mapper);
      });
      services.AddSingleton<ILogTypeRepository>(r =>
      {
        var connector = r.GetService<IConnector>();
        var mapper = r.GetService<IMapper>();
        var cache = r.GetService<IMemoryCache>();
        return new LogTypeRepository(connector, mapper, cache);
      });

      return services;
    }

    public static IServiceCollection AddSevices(this IServiceCollection services)
    {
      services.AddScoped<ILogService>(r =>
      {
        var connector = r.GetService<IConnector>();
        var mapper = r.GetService<IMapper>();
        var logRepository = r.GetService<ILogRepository>();
        var customAttributeRepository = r.GetService<ICustomAttributeRepository>();
        var logTypeRepository = r.GetService<ILogTypeRepository>();
        return new LogService(connector, mapper, logRepository, customAttributeRepository, logTypeRepository);
      });

      return services;
    }
  }
}