using AutoMapper;
using Dapper.FluentMap;
using NUnit.Framework;
using SmartLog.DAL.Map;
using SmartLog.DAL.Repository;
using SmartLog.Domain;
using SmartLog.Domain.Dto;
using SmartLog.Domain.Interfaces;
using SmartLog.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLog.Tests
{
  public class RepositoryTests
  {
    private static readonly string _connectionString =
      @"Server=HOME-LAP\SQLEXPRESS;Database=smartLog;Trusted_Connection=True;";

    private IConnector _connector;
    private IMapper _mapper;
    private ILogRepository _logRepository;
    private ICustomAttributeRepository _customAttributeRepository;

    private static LogDto GetLogDto()
    {
      return new LogDto
      {
        CreateDate = DateTime.Now,
        MethodName = "execute1",
        Parent = 1,
        Type = 2,
        Uid = Guid.NewGuid(),
        Message = "Сообщение 1"
      };
    }

    private static CustomAttributeDto GetCustomAttributeDto(long logsId)
    {
      return new CustomAttributeDto
      {
        LogsId = logsId,
        Name = "name1",
        Value = "value1"
      };
    }

    [SetUp]
    public void Setup()
    {
      _connector = new Connector(_connectionString);
      var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperProfile()); });

      _mapper = new Mapper(config);
      _logRepository = new LogRepository(_mapper);
      _customAttributeRepository = new CustomAttributeRepository(_mapper);
      var entityMaps = FluentMapper.EntityMaps;
      if (entityMaps.Count == 0)
        FluentMapper.Initialize(c =>
        {
          c.AddMap(new LogEntityMap());
          c.AddMap(new CustomAttributeEntityMap());
          c.AddMap(new LogTypeEntityMap());
        });
    }

    [Test]
    public async Task LogsTestAsync()
    {
      await using var connection1 = _connector.GetConnection();
      await _logRepository.ClearAsync(connection1);

      await using var connection2 = _connector.GetConnection();
      await using var transaction = connection2.BeginTransaction();

      var baseNow = DateTime.Now;
      try
      {
        var value = GetLogDto();
        var result1 = await _logRepository.InsertAsync(value, connection2, transaction);
        if (result1 == 0L)
          throw new InvalidOperationException("Ошибка добавления записи в [logs].");
        transaction.Commit();
      }
      catch
      {
        transaction.Rollback();
        throw;
      }

      var result2 = await _logRepository.GetAsync(baseNow, DateTime.Now, connection1);
      var methodName = result2.First().MethodName;
      if (methodName != "execute1")
        throw new InvalidOperationException($"Неверное значение MethodName ({methodName}).");

      await _logRepository.ClearAsync(connection1);

      Assert.Pass("Тест пройден.");
    }

    [Test]
    public async Task CustomAttributeTestAsync()
    {
      await using var connection1 = _connector.GetConnection();
      await _logRepository.ClearAsync(connection1);
      await _customAttributeRepository.ClearAsync(connection1);

      await using var connection2 = _connector.GetConnection();
      await using var transaction = connection2.BeginTransaction();

      var baseNow = DateTime.Now;

      long logsId;
      try
      {
        var value1 = GetLogDto();
        logsId = await _logRepository.InsertAsync(value1, connection2, transaction);

        var value2 = GetCustomAttributeDto(logsId);
        var customAttributeId = await _customAttributeRepository.InsertAsync(value2, connection2, transaction);
        if (customAttributeId == 0L)
          throw new InvalidOperationException("Ошибка добавления записи в [custom_attributes].");
        transaction.Commit();
      }
      catch
      {
        transaction.Rollback();
        throw;
      }

      var result2 = await _customAttributeRepository.GetAsync(new long[] {logsId}, connection1);
      var name = result2.First().Name;
      if (name != "name1")
        throw new InvalidOperationException($"Неверное значение name ({name}).");

      await _logRepository.ClearAsync(connection1);
      await _customAttributeRepository.ClearAsync(connection1);

      Assert.Pass("Тест пройден.");
    }
  }
}