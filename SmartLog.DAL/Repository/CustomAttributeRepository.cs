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
  public class CustomAttributeRepository : ICustomAttributeRepository
  {
    private readonly IConnector _connector;
    private readonly IMapper _mapper;

    public CustomAttributeRepository(IConnector connector, IMapper mapper)
    {
      _connector = connector ?? throw new ArgumentNullException(nameof(connector));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<CustomAttributeDto>> GetAsync(IEnumerable<long> logIds)
    {
      await using var connection = _connector.GetConnection();
      var entities = await connection.QueryAsync<LogDataEntity>(
        "select [custom_attributes_id], [logs_id], [name], [value] from [custom_attributes] " +
        $"where[logs_id] in ({string.Join(',', logIds)})");

      return _mapper.Map<IEnumerable<CustomAttributeDto>>(entities);
    }

    public async Task<int> InsertAsync(IEnumerable<CustomAttributeDto> values)
    {
      await using var connection = _connector.GetConnection();
      using var transaction = connection.BeginTransaction();
      var totalCount = 0;
      foreach (var value in values)
      {
        try
        {
          totalCount += await connection.ExecuteAsync(
            "insert into log_data ([logs_id], [name], [value]) " + 
            "values(:pLogsId, :pName, :pValue)", 
            new { value.LogsId, value.Name, value.Value });
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
