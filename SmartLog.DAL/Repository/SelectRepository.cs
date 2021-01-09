using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using SmartLog.DAL.Entities;
using SmartLog.Domain.Dto;
using SmartLog.Domain.Interfaces;

namespace SmartLog.DAL.Repository
{
  public class SelectRepository : ISelectRepository
  {
    private readonly IMapper _mapper;

    public SelectRepository(IMapper mapper)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<SelectDto>> GetByDateRangeAsync(DateTime initial, DateTime final, SqlConnection connection)
    {
      var entities = await connection.QueryAsync<SelectEntity>(Sql.SelectByDateRange,
        new {pInitial = initial, pFinal = final});

      return _mapper.Map<IEnumerable<SelectDto>>(entities);
    }
  }
}