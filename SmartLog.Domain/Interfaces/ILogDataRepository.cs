using SmartLog.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLog.Domain.Interfaces
{
  public interface ILogDataRepository
  {
    Task<IEnumerable<LogDataDto>> GetAsync(IEnumerable<long> logIds);

    Task<int> InsertAsync(IEnumerable<LogDataDto> values);
  }
}
