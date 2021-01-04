using SmartLog.Domain.Dto;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SmartLog.Domain.Interfaces
{
  public interface ICustomAttributeRepository : IRepository
  {
    Task<IEnumerable<CustomAttributeDto>> GetAsync(IEnumerable<long> logIds, SqlConnection connection);

    Task<long> InsertAsync(CustomAttributeDto value, SqlConnection connection, SqlTransaction transaction);
  }
}