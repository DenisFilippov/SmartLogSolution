using SmartLog.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLog.Domain.Interfaces
{
  public interface ILogRepository : IRepository
  {
    Task<IEnumerable<LogDto>> GetAsync(DateTime initial, DateTime final, SqlConnection connection);

    Task<long> InsertAsync(LogDto value, SqlConnection connection, SqlTransaction transaction);
  }
}