using System;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using SmartLog.Core;
using SmartLog.Domain;
using SmartLog.Domain.Dto;
using SmartLog.Domain.Interfaces;
using SmartLog.Mapping;

namespace SmartLog.Service
{
  public class LogService : ILogService
  {
    private readonly IConnector _connector;
    private readonly ILogRepository _logRepository;
    private readonly ICustomAttributeRepository _customAttributeRepository;
    private readonly ILogTypeRepository _logTypeRepository;
    private readonly IMapper _mapper;

    private static SmartLogResponse CreateResponse(int code, Guid? uid = null, string message = null)
    {
      return (uid == null && string.IsNullOrEmpty(message))
        ? new SmartLogResponse
        {
          Code = code,
          SmartLogError = null
        }
        : new SmartLogResponse
        {
          Code = code,
          SmartLogError = new SmartLogErrorDto
          {
            UId = uid ?? Guid.Empty,
            Message = message
          }
        };
    }

    private async Task CreateNestedLogAsync(SmartLogDto smartLog, LogDto log,
      SqlConnection connection, SqlTransaction transaction)
    {
      try
      {
        if (smartLog.CustomAttributes != null && smartLog.CustomAttributes.Any())
        {
          foreach (var smartLogCustomAttribute in smartLog.CustomAttributes)
          {
            var customAttribute = _mapper.Map<CustomAttributeDto>(smartLogCustomAttribute, opt
              => opt.Items[MapperProfile.LOGS_ID] = log.Id);
            customAttribute.Id =
              await _customAttributeRepository.InsertAsync(customAttribute, connection, transaction);
          }
        }

        if (smartLog.Children != null && smartLog.Children.Any())
        {
          foreach (var nestedSmartLog in smartLog.Children)
          {
            var nestedLog = _mapper.Map<LogDto>(nestedSmartLog, opt
              => opt.Items.Add(MapperProfile.LOG_TYPE_REPOSITORY, _logTypeRepository));
            nestedLog.Parent = log.Id;
            nestedLog.Id = await _logRepository.InsertAsync(nestedLog, connection, transaction);
            await CreateNestedLogAsync(nestedSmartLog, nestedLog, connection, transaction);
          }
        }
      }
      catch (Exception ex)
      {
        throw new CreateLogException(smartLog.UId, ex.Message, ex);
      }
    }

    public LogService(IConnector connector, IMapper mapper, ILogRepository logRepository,
      ICustomAttributeRepository customAttributeRepository,
      ILogTypeRepository logTypeRepository)
    {
      _connector = connector ?? throw new ArgumentNullException(nameof(connector));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
      _customAttributeRepository = customAttributeRepository ??
                                   throw new ArgumentNullException(nameof(customAttributeRepository));
      _logTypeRepository = logTypeRepository ?? throw new ArgumentNullException(nameof(logTypeRepository));
    }

    public async Task<SmartLogResponse> CreateLogAsync(string type, string uid,
      string methodName, string createDate, string message)
    {
      var guid = !string.IsNullOrEmpty(uid) ? Guid.Parse(uid) : Guid.Empty;

      await using var connection = _connector.GetConnection();
      await using var transaction = connection.BeginTransaction();

      try
      {
        var value = new LogDto
        {
          CreateDate = DateTime.Parse(createDate),
          Message = message,
          MethodName = methodName,
          Parent = 0,
          Type = (await _logTypeRepository.GetAsync()).First(r => r.Name == type).Id,
          Uid = guid
        };
        await _logRepository.InsertAsync(value, connection, transaction);
        transaction.Commit();
        return CreateResponse(200);
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        return CreateResponse(500, guid, ex.Message);
      }
    }

    public async Task<SmartLogResponse> CreateLogAsync(SmartLogRequest request)
    {
      await using var connection = _connector.GetConnection();
      await using var transaction = connection.BeginTransaction();

      Guid? uid = null;

      try
      {
        foreach (var smartLog in request.Logs)
        {
          uid = smartLog.UId;
          var log = _mapper.Map<LogDto>(smartLog, opt 
            => opt.Items.Add(MapperProfile.LOG_TYPE_REPOSITORY, _logTypeRepository));
          log.Parent = 0L;
          log.Id = await _logRepository.InsertAsync(log, connection, transaction);

          await CreateNestedLogAsync(smartLog, log, connection, transaction);
        }
        transaction.Commit();
        return CreateResponse(200);
      }
      catch (CreateLogException ex)
      {
        transaction.Rollback();
        return CreateResponse(500, ex.UId, ex.Message);
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        return CreateResponse(500, uid, ex.Message);
      }
    }

    public SmartLogInfoResponse GetServiceInfo()
    {
      var version = Assembly.GetCallingAssembly().GetName().Version;
      return (version != null)
        ? new SmartLogInfoResponse {Version = version.ToString()}
        : new SmartLogInfoResponse {Version = string.Empty};
    }
  }
}