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
  public class CustomAttributeRepository : ICustomAttributeRepository
  {
    private readonly IMapper _mapper;

    public CustomAttributeRepository(IMapper mapper)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<CustomAttributeDto>> GetAsync(IEnumerable<long> logIds, SqlConnection connection)
    {
      var entities = await connection.QueryAsync<CustomAttributeEntity>(
        string.Format(Sql.SelectCustomAttributes, string.Join(',', logIds)));

      return _mapper.Map<IEnumerable<CustomAttributeDto>>(entities);
    }

    public async Task<long> InsertAsync(CustomAttributeDto value, SqlConnection connection, SqlTransaction transaction)
    {
      await using var command = new SqlCommand(Sql.InsertCustomAttributes, connection, transaction);
      command.Parameters.AddWithValue("@pLogsId", value.LogsId);
      command.Parameters.AddWithValue("@pName", value.Name);
      command.Parameters.AddWithValue("@pValue", value.Value);
      command.Parameters.Add(new SqlParameter
      {
        ParameterName = "@pCustomAttributesId",
        DbType = DbType.Int64,
        Direction = ParameterDirection.Output
      });
      await command.ExecuteNonQueryAsync();

      return (long)command.Parameters["@pCustomAttributesId"].Value;
    }

    public async Task ClearAsync(SqlConnection connection)
    {
      await connection.ExecuteAsync(Sql.DeleteCustomAttributes);
    }
  }
}