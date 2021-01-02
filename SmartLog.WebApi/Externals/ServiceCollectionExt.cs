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
      services.AddSingleton<IConfigDto>(r => new ConfigDto
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
      services.AddSingleton<IMapper>(r =>
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
        c.AddMap(new LogDataEntityMap());
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
        var connector = r.GetService<IConnector>();
        var mapper = r.GetService<IMapper>();
        return new LogRepository(connector, mapper);
      });
      services.AddScoped<ILogDataRepository>(r =>
      {
        var connector = r.GetService<IConnector>();
        var mapper = r.GetService<IMapper>();
        return new LogDataRepository(connector, mapper);
      });
      services.AddScoped<ICustomAttributeRepository>(r =>
      {
        var connector = r.GetService<IConnector>();
        var mapper = r.GetService<IMapper>();
        return new CustomAttributeRepository(connector, mapper);
      });
      services.AddScoped<ILogTypeRepository>(r =>
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
        var logRepository = r.GetService<ILogRepository>();
        var logDataRepository = r.GetService<ILogDataRepository>();
        var customAttributeRepository = r.GetService<ICustomAttributeRepository>();
        var logTypeRepository = r.GetService<ILogTypeRepository>();
        return new LogService(logRepository, logDataRepository, customAttributeRepository, logTypeRepository);
      });

      return services;
    }
  }
}