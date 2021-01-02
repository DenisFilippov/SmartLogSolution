using Dapper.FluentMap.Mapping;
using SmartLog.DAL.Entities;

namespace SmartLog.DAL.Map
{
  public class LogDataEntityMap : EntityMap<LogDataEntity>
  {
    public LogDataEntityMap()
    {
      Map(r => r.Id).ToColumn("logs_data_id");
      Map(r => r.LogsId).ToColumn("logs_id");
      Map(r => r.Key).ToColumn("data_key");
      Map(r => r.Value).ToColumn("data_value");
    }
  }
}