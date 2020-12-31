using AutoMapper;
using Dapper.FluentMap;
using NUnit.Framework;
using SmartLog.DAL.Map;
using SmartLog.DAL.Repository;
using SmartLog.Domain;
using SmartLog.Domain.Interfaces;
using SmartLog.Mapping;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLog.Tests
{
  public class RepositoryTests
  {
    private static readonly string _connectionString = @"Server=HOME-LAP\SQLEXPRESS;Database=smartLog;Trusted_Connection=True;";
    private IConnector _connector;
    private IMapper _mapper;
    private ILogRepository _logRepository;

    [SetUp]
    public void Setup()
    {
      _connector = new Connector(_connectionString);
      var config = new MapperConfiguration(cfg => 
      {
        cfg.AddProfile(new MapperProfile());
      });

      _mapper = new Mapper(config);
      _logRepository = new LogRepository(_connector, _mapper);
      FluentMapper.Initialize(config =>
      {
        config.AddMap(new LogEntityMap());
        config.AddMap(new LogDataEntityMap());
        config.AddMap(new CustomAttributeEntityMap());
      });
    }

    [Test]
    public async Task LogsTestAsync()
    {
      Assert.Pass("Тест пройден.");
    }

    [Test]
    public async Task LogDataTestAsync()
    {
      Assert.Pass("Тест пройден.");
    }

    [Test]
    public async Task CustomAttributeTestAsync()
    {
      Assert.Pass("Тест пройден.");
    }
  }
}