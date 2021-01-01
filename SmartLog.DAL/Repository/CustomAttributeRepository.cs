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
      var entities = await connection.QueryAsync<CustomAttributeEntity>(
        string.Format(Sql.SelectCustomAttributes, string.Join(',', logIds)));

      return _mapper.Map<IEnumerable<CustomAttributeDto>>(entities);
    }

    public async Task<int> InsertAsync(IEnumerable<CustomAttributeDto> values)
    {
      await using var connection = _connector.GetConnection();
      await using var transaction = connection.BeginTransaction();
      var totalCount = 0;
      foreach (var value in values)
      {
        try
        {
          totalCount += await connection.ExecuteAsync(Sql.InsertCustomAttributes,
            new { pLogsId = value.LogsId, pName = value.Name, pValue = value.Value }, 
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
      await connection.ExecuteAsync(Sql.DeleteCustomAttributes);
    }
  }
}
