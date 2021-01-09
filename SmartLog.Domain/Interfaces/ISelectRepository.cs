using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartLog.Domain.Dto;

namespace SmartLog.Domain.Interfaces
{
  public interface ISelectRepository
  {
    Task<IEnumerable<SelectDto>> GetByDateRangeAsync(DateTime initial, DateTime final, SqlConnection connection);
  }
}
