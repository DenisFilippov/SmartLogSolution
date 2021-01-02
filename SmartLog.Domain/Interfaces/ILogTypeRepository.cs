using System.Collections.Generic;
using System.Threading.Tasks;
using SmartLog.Domain.Dto;

namespace SmartLog.Domain.Interfaces
{
  public interface ILogTypeRepository
  {
    Task<IEnumerable<LogTypeDto>> GetAsync();
  }
}