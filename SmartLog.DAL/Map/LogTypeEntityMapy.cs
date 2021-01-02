using Dapper.FluentMap.Mapping;
using SmartLog.DAL.Entities;

namespace SmartLog.DAL.Map
{
  public class LogTypeEntityMap : EntityMap<LogTypeEntity>
  {
    public LogTypeEntityMap()
    {
      Map(r => r.Id).ToColumn("log_types_id");
      Map(r => r.Name).ToColumn("name");
    }
  }
}