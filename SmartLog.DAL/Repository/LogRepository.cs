using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    private readonly IMapper _mapper;

    public LogRepository(IMapper mapper)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<LogDto>> GetAsync(DateTime initial, DateTime final, SqlConnection connection)
    {
      var entities = await connection.QueryAsync<LogEntity>(Sql.SelectLogs,
        new {pInitial = initial, pFinal = final});

      return _mapper.Map<IEnumerable<LogDto>>(entities);
    }

    public async Task<long> InsertAsync(LogDto value, SqlConnection connection, SqlTransaction transaction)
    {
      await using var command = new SqlCommand(Sql.InsertLogs, connection, transaction);
      command.Parameters.AddWithValue("@pCreateDate", value.CreateDate);
      command.Parameters.AddWithValue("@pLogGuid", value.Uid);
      command.Parameters.AddWithValue("@pMessage", value.Message);
      command.Parameters.AddWithValue("@pMethodName", value.MethodName);
      command.Parameters.AddWithValue("@pParent", value.Parent);
      command.Parameters.AddWithValue("@pType", value.Type);
      command.Parameters.Add(new SqlParameter
      {
        ParameterName = "@pLogsId",
        DbType = DbType.Int64,
        Direction = ParameterDirection.Output
      });
      await command.ExecuteNonQueryAsync();

      return (long)command.Parameters["@pLogsId"].Value;
    }

    public async Task ClearAsync(SqlConnection connection)
    {
      await using var command = new SqlCommand(Sql.DeleteLogs, connection);
      await command.ExecuteNonQueryAsync();
    }
  }
}