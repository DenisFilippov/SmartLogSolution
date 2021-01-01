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
    private ILogDataRepository _logDataRepository;
    private ICustomAttributeRepository _customAttributeRepository;

    private LogDto[] Get1LogDtoArray()
    {
      var result = new LogDto[1];
      result[0] = new LogDto
      {
        CreateDate = DateTime.Now,
        MethodName = "execute1",
        Parent = 1,
        Type = 2,
        Uid = Guid.NewGuid(),
        Message = "Сообщение 1"
      };

      return result;
    }

    private LogDto[] Get3LogDtoArray()
    {
      var result = new LogDto[3];
      result[0] = new LogDto
      {
        CreateDate = DateTime.Now,
        MethodName = "execute1",
        Parent = 1,
        Type = 2,
        Uid = Guid.NewGuid(),
        Message = "Сообщение 1"
      };
      result[1] = new LogDto
      {
        CreateDate = DateTime.Now,
        MethodName = "execute2",
        Parent = 1,
        Type = 2,
        Uid = Guid.NewGuid(),
        Message = "Сообщение 2"
      };
      result[2] = new LogDto
      {
        CreateDate = DateTime.Now,
        MethodName = "execute3",
        Parent = 1,
        Type = 2,
        Uid = Guid.NewGuid(),
        Message = "Сообщение 3"
      };

      return result;
    }

    private LogDataDto[] Get3LogDataDtoArray(long logsId)
    {
      var result = new LogDataDto[3];
      result[0] = new LogDataDto
      {
        LogsId = logsId,
        Key = "key1",
        Value = new byte[] {0x01, 0x02, 0x03}
      };
      result[1] = new LogDataDto
      {
        LogsId = logsId,
        Key = "key2",
        Value = new byte[] {0x11, 0x12, 0x13}
      };
      result[2] = new LogDataDto
      {
        LogsId = logsId,
        Key = "key3",
        Value = new byte[] {0x21, 0x22, 0x23}
      };

      return result;
    }

    private CustomAttributeDto[] Get3CustomAttributeDtoArray(long logsId)
    {
      var result = new CustomAttributeDto[3];
      result[0] = new CustomAttributeDto
      {
        LogsId = logsId,
        Name = "name1",
        Value = "value1"
      };
      result[1] = new CustomAttributeDto
      {
        LogsId = logsId,
        Name = "name2",
        Value = "value2"
      };
      result[2] = new CustomAttributeDto
      {
        LogsId = logsId,
        Name = "name3",
        Value = "value3"
      };
      return result;
    }

    [SetUp]
    public void Setup()
    {
      _connector = new Connector(_connectionString);
      var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperProfile()); });

      _mapper = new Mapper(config);
      _logRepository = new LogRepository(_connector, _mapper);
      _logDataRepository = new LogDataRepository(_connector, _mapper);
      _customAttributeRepository = new CustomAttributeRepository(_connector, _mapper);
      var entityMaps = FluentMapper.EntityMaps;
      if (entityMaps.Count == 0)
      {
        FluentMapper.Initialize(config =>
        {
          config.AddMap(new LogEntityMap());
          config.AddMap(new LogDataEntityMap());
          config.AddMap(new CustomAttributeEntityMap());
        });
      }
    }

    [Test]
    public async Task LogsTestAsync()
    {
      await _logRepository.ClearAsync();

      var baseNow = DateTime.Now;
      var values = Get3LogDtoArray();
      var result1 = await _logRepository.InsertAsync(values);
      if (result1 != 3)
        throw new InvalidOperationException($"Количество добавленных записей неверное ({result1}).");
      var result2 = await _logRepository.GetAsync(baseNow, DateTime.Now);
      if (result2.Count() != 3)
        throw new InvalidOperationException($"Количество возвращённых записей неверное ({result2.Count()}).");
      var methodName = result2.Last().MethodName;
      if (methodName != "execute3")
        throw new InvalidOperationException($"Неверное значение MethodName ({methodName}).");
      await _logRepository.ClearAsync();
      Assert.Pass("Тест пройден.");
    }

    [Test]
    public async Task LogDataTestAsync()
    {
      await _logRepository.ClearAsync();
      await _logDataRepository.ClearAsync();

      var baseNow = DateTime.Now;
      var values1 = Get1LogDtoArray();
      await _logRepository.InsertAsync(values1);
      var log = (await _logRepository.GetAsync(baseNow, DateTime.Now)).FirstOrDefault();
      if (log == null)
        throw new InvalidOperationException("Количество добавленных записей в [logs] неверное.");
      var values2 = Get3LogDataDtoArray(log.Id);
      var result1 = await _logDataRepository.InsertAsync(values2);
      if (result1 != 3)
        throw new InvalidOperationException($"Количество добавленных записей неверное ({result1}).");
      var result2 = await _logDataRepository.GetAsync(new long[] {log.Id});
      if (result2.Count() != 3)
        throw new InvalidOperationException($"Количество возвращённых записей неверное ({result2.Count()}).");
      var key = result2.Last().Key;
      if (key != "key3")
        throw new InvalidOperationException($"Неверное значение key ({key}).");
      await _logRepository.ClearAsync();
      await _logDataRepository.ClearAsync();
      Assert.Pass("Тест пройден.");
    }

    [Test]
    public async Task CustomAttributeTestAsync()
    {
      await _logRepository.ClearAsync();
      await _customAttributeRepository.ClearAsync();

      var baseNow = DateTime.Now;
      var values1 = Get1LogDtoArray();
      await _logRepository.InsertAsync(values1);
      var log = (await _logRepository.GetAsync(baseNow, DateTime.Now)).FirstOrDefault();
      if (log == null)
        throw new InvalidOperationException("Количество добавленных записей в [logs] неверное.");
      var values2 = Get3CustomAttributeDtoArray(log.Id);
      var result1 = await _customAttributeRepository.InsertAsync(values2);
      if (result1 != 3)
        throw new InvalidOperationException($"Количество добавленных записей неверное ({result1}).");
      var result2 = await _customAttributeRepository.GetAsync(new long[] {log.Id});
      if (result2.Count() != 3)
        throw new InvalidOperationException($"Количество возвращённых записей неверное ({result2.Count()}).");
      var name = result2.Last().Name;
      if (name != "name3")
        throw new InvalidOperationException($"Неверное значение name ({name}).");
      await _logRepository.ClearAsync();
      await _customAttributeRepository.ClearAsync();
      Assert.Pass("Тест пройден.");
    }
  }
}