using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartLog.WebApi.Externals;

namespace SmartLog.WebApi
{
  public class Startup
  {
    private readonly IConfiguration _config;

    public Startup(IConfiguration config)
    {
      _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddMvcCore(options => options.EnableEndpointRouting = false)
        .AddXmlSerializerFormatters()
        ;

      services
        .AddConfig(_config)
        .AddMapper()
        .AddConnector()
        .AddRepositories()
        .AddSevices()
        .AddFluentMapper()
        .AddMemoryCache()
        ;
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

      app.UseRouting();
      app.UseMvc();
    }
  }
}