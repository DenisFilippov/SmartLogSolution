using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartLog.Domain.Dto;
using SmartLog.Domain.Interfaces;

namespace SmartLog.Service
{
  public class LogService : ILogService
  {
    private readonly ILogRepository _logRepository;
    private readonly ILogDataRepository _logDataRepository;
    private readonly ICustomAttributeRepository _customAttributeRepository;
    private readonly ILogTypeRepository _logTypeRepository;

    private SmartLogResponse CreateResponse(int code, Guid? uid = null, string message = null)
    {
      return uid == null
        ? new SmartLogResponse
        {
          Code = code,
          Error = null
        }
        : new SmartLogResponse
        {
          Code = code,
          Error = new ErrorDto
          {
            UId = uid.Value,
            Message = message
          }
        };
    }

    public LogService(ILogRepository logRepository,
      ILogDataRepository logDataRepository, ICustomAttributeRepository customAttributeRepository,
      ILogTypeRepository logTypeRepository)
    {
      _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
      _logDataRepository = logDataRepository ?? throw new ArgumentNullException(nameof(logDataRepository));
      _customAttributeRepository = customAttributeRepository ??
                                   throw new ArgumentNullException(nameof(customAttributeRepository));
      _logTypeRepository = logTypeRepository ?? throw new ArgumentNullException(nameof(logTypeRepository));
    }

    public async Task<SmartLogResponse> CreateLogAsync(string type, string uid,
      string methodName, string createDate, string message)
    {
      var guid = Guid.Parse(uid);

      try
      {
        var logs = new LogDto[1];
        logs[0] = new LogDto
        {
          CreateDate = DateTime.Parse(createDate),
          Message = message,
          MethodName = methodName,
          Parent = 0,
          Type = (await _logTypeRepository.GetAsync()).First(r => r.Name == type).Id,
          Uid = guid
        };
        await _logRepository.InsertAsync(logs);
        return CreateResponse(200);
      }
      catch (Exception ex)
      {
        return CreateResponse(500, guid, ex.Message);
      }
    }

    public async Task<SmartLogResponse> CreateLogAsync(SmartLogRequest request)
    {
      throw new NotImplementedException();
    }
  }
}