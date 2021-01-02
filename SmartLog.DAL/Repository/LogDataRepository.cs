using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using SmartLog.DAL.Entities;
using SmartLog.Domain.Dto;
using SmartLog.Domain.Interfaces;

namespace SmartLog.DAL.Repository
{
  public class LogDataRepository : ILogDataRepository
  {
    private readonly IConnector _connector;
    private readonly IMapper _mapper;

    public LogDataRepository(IConnector connector, IMapper mapper)
    {
      _connector = connector ?? throw new ArgumentNullException(nameof(connector));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<LogDataDto>> GetAsync(IEnumerable<long> logIds)
    {
      await using var connection = _connector.GetConnection();
      var entities = await connection.QueryAsync<LogDataEntity>(
        string.Format(Sql.SelectLogData, string.Join(',', logIds)));

      return _mapper.Map<IEnumerable<LogDataDto>>(entities);
    }

    public async Task<int> InsertAsync(IEnumerable<LogDataDto> values)
    {
      await using var connection = _connector.GetConnection();
      await using var transaction = connection.BeginTransaction();
      var totalCount = 0;
      foreach (var value in values)
        try
        {
          totalCount += await connection.ExecuteAsync(Sql.InsertLogData,
            new {pLogsId = value.LogsId, pDataKey = value.Key, pDataValue = value.Value},
            transaction);
        }
        catch (Exception)
        {
          transaction.Rollback();
          throw;
        }

      transaction.Commit();

      return totalCount;
    }

    public async Task ClearAsync()
    {
      await using var connection = _connector.GetConnection();
      await connection.ExecuteAsync(Sql.DeleteLogData);
    }
  }
}