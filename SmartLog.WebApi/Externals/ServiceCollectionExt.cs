using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartLog.DAL.Repository;
using SmartLog.Domain;
using SmartLog.Domain.Dto;
using SmartLog.Domain.Interfaces;
using SmartLog.Mapping;

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

      return services;
    }
  }
}