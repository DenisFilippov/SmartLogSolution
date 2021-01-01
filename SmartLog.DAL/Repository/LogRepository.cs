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
  public class LogRepository : ILogRepository
  {
    private readonly IConnector _connector;
    private readonly IMapper _mapper;

    public LogRepository(IConnector connector, IMapper mapper)
    {
      _connector = connector ?? throw new ArgumentNullException(nameof(connector));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<LogDto>> GetAsync(DateTime initial, DateTime final)
    {
      await using var connection = _connector.GetConnection();
      var entities = await connection.QueryAsync<LogEntity>(Sql.SelectLogs, 
        new { pInitial = initial, pFinal = final });

      return _mapper.Map<IEnumerable<LogDto>>(entities);
    }

    public async Task<int> InsertAsync(IEnumerable<LogDto> values)
    {
      await using var connection = _connector.GetConnection();
      using var transaction = connection.BeginTransaction();
      var totalCount = 0;
      foreach (var value in values)
      {
        try
        {
          totalCount += await connection.ExecuteAsync(Sql.InsertLogs, 
            new { pCreateDate = value.CreateDate, pLogGuid = value.Uid, pMessage = value.Message, 
              pMethodName = value.MethodName, pParent = value.Parent, pType = value.Type },
            transaction);
        }
        catch (Exception)
        {
          transaction.Rollback();
          throw;
        }
      }
      transaction.Commit();

      return totalCount;
    }

    public async Task ClearAsync()
    {
      await using var connection = _connector.GetConnection();
      await connection.ExecuteAsync(Sql.DeleteLogs);
    }
  }
}
