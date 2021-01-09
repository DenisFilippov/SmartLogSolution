using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SmartLog.Domain;
using SmartLog.Domain.Dto;
using SmartLog.Domain.Interfaces;
using SmartLog.Mapping;

namespace SmartLog.Service
{
  public class SelectService : ISelectService
  {
    private readonly IConnector _connector;
    private readonly ILogTypeRepository _logTypeRepository;
    private readonly ISelectRepository _selectRepository;
    private readonly IMapper _mapper;

    private void CreateNestedLog(SelectDto[] rawResultArray, IGrouping<Guid, SelectDto> grouped, SmartLogDto smartLog)
    {
      try
      {
        if (grouped.Any(r => !string.IsNullOrEmpty(r.Name)))
        {
          smartLog.CustomAttributes ??= new SmartCustomAttributeDto[grouped.Count()];
          var index = 0;
          foreach (var item in grouped)
          {
            smartLog.CustomAttributes[index++] = _mapper.Map<SmartCustomAttributeDto>(item);
          }
        }

        var childGrouped = rawResultArray.Where(r => r.Parent == grouped.First().LogsId).GroupBy(r => r.LogGuid).ToArray();
        if (childGrouped.Any())
        {
          smartLog.Children ??= new SmartLogDto[childGrouped.Count()];
          var index = 0;
          foreach (var groupedItem in childGrouped)
          {
            var childSmartLog = _mapper.Map<SmartLogDto>(groupedItem.First(), opt
              =>
            {
              opt.Items.Add(MapperProfile.LOG_TYPE_REPOSITORY, _logTypeRepository);
            });

            CreateNestedLog(rawResultArray, groupedItem, childSmartLog);
            smartLog.Children[index++] = childSmartLog;
          }
        }
      }
      catch (Exception ex)
      {
        throw new CreateLogException(smartLog.UId, ex.Message, ex);
      }
    }

    public SelectService(IConnector connector, IMapper mapper,
      ILogTypeRepository logTypeRepository, ISelectRepository selectRepository)
    {
      _connector = connector ?? throw new ArgumentNullException(nameof(connector));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _logTypeRepository = logTypeRepository ?? throw new ArgumentNullException(nameof(logTypeRepository));
      _selectRepository = selectRepository ?? throw new ArgumentNullException(nameof(selectRepository));
    }

    public async Task<SmartLogSelectResponse> GetByDateRangeAsync(DateTime initial, DateTime final)
    {
      await using var connection = _connector.GetConnection();

      try
      {
        var rawResultArray = (await _selectRepository.GetByDateRangeAsync(initial, final, connection)).ToArray();
        var logs = new List<SmartLogDto>();
        foreach (var groupedItem in rawResultArray.Where(r => r.Parent == 0L).GroupBy(r => r.LogGuid))
        {
          var smartLog = _mapper.Map<SmartLogDto>(groupedItem.First(), opt
            =>
          {
            opt.Items.Add(MapperProfile.LOG_TYPE_REPOSITORY, _logTypeRepository);
          });

          CreateNestedLog(rawResultArray, groupedItem, smartLog);
          logs.Add(smartLog);
        }

        return new SmartLogSelectResponse
        {
          Code = 200,
          Error = null,
          Logs = logs.ToArray()
        };
      }
      catch (CreateLogException ex)
      {
        return new SmartLogSelectResponse
        {
          Code = 500,
          Logs = null,
          Error = new ErrorDto
          {
            Message = ex.Message,
            UId = ex.UId ?? Guid.Empty,
          }
        };
      }
      catch (Exception ex)
      {
        return new SmartLogSelectResponse
        {
          Code = 500,
          Logs = null,
          Error = new ErrorDto
          {
            Message = ex.Message,
            UId = Guid.Empty,
          }
        };
      }
    }
  }
}