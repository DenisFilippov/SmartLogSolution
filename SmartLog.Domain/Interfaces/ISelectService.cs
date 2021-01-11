using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartLog.Core;
using SmartLog.Domain.Dto;

namespace SmartLog.Domain.Interfaces
{
  public interface ISelectService
  {
    Task<SmartLogSelectResponse> GetByDateRangeAsync(DateTime initial, DateTime final);
  }
}
