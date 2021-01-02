using SmartLog.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLog.Domain.Interfaces
{
  public interface ICustomAttributeRepository : IRepository
  {
    Task<IEnumerable<CustomAttributeDto>> GetAsync(IEnumerable<long> logIds);

    Task<int> InsertAsync(IEnumerable<CustomAttributeDto> values);
  }
}