using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
      var entities = await connection.QueryAsync<LogEntity>(
        "select [logs_id], [log_guid], [parent], [create_date], [type], [method_name], [message] " +
        "from [logs] where [create_date] >= @initial and " +
        "[create_date] <= @final order by [create_date]", new { initial, final });

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
          totalCount += await connection.ExecuteAsync(
            "insert into logs ([create_date], [log_guid], [message], [method_name], [parent], [type]) " +
            "values(:pCreate_date, :pLog_guid, :pMessage, :pMethod_name, :pParent, :pType)", 
            new { value.CreateDate, value.Uid, value.Message, value.MethodName, value.Parent, value.Type });
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
  }
}
