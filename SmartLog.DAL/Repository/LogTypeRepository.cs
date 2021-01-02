using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Caching.Memory;
using SmartLog.DAL.Entities;
using SmartLog.Domain.Dto;
using SmartLog.Domain.Interfaces;

namespace SmartLog.DAL.Repository
{
  public class LogTypeRepository : ILogTypeRepository
  {
    private readonly IConnector _connector;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public LogTypeRepository(IConnector connector, IMapper mapper, IMemoryCache cache)
    {
      _connector = connector ?? throw new ArgumentNullException(nameof(connector));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<IEnumerable<LogTypeDto>> GetAsync()
    {
      const string LOG_TYPES = "LOG_TYPES";

      if (_cache.TryGetValue(LOG_TYPES, out LogTypeDto[] result)) return result;

      await using var connection = _connector.GetConnection();
      var entities = await connection.QueryAsync<LogTypeEntity>(Sql.SelectLogTypes);
      result = _mapper.Map<IEnumerable<LogTypeDto>>(entities).ToArray();
      _cache.Set(LOG_TYPES, result);
      return result;
    }
  }
}