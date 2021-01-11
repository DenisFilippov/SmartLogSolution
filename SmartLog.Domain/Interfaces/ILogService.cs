using System;
using System.Threading.Tasks;
using SmartLog.Core;
using SmartLog.Domain.Dto;

namespace SmartLog.Domain.Interfaces
{
  public interface ILogService
  {
    Task<SmartLogResponse> CreateLogAsync(string type, string uid, string methodName, string createDate,
      string message);

    Task<SmartLogResponse> CreateLogAsync(SmartLogRequest request);

    SmartLogInfoResponse GetServiceInfo();
  }
}