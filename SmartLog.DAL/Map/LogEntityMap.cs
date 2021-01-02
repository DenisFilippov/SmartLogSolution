using Dapper.FluentMap.Mapping;
using SmartLog.DAL.Entities;

namespace SmartLog.DAL.Map
{
  public class LogEntityMap : EntityMap<LogEntity>
  {
    public LogEntityMap()
    {
      Map(r => r.Id).ToColumn("logs_id");
      Map(r => r.Uid).ToColumn("log_guid");
      Map(r => r.CreateDate).ToColumn("create_date");
      Map(r => r.Message).ToColumn("message");
      Map(r => r.Parent).ToColumn("perent");
      Map(r => r.Type).ToColumn("type");
      Map(r => r.MethodName).ToColumn("method_name");
    }
  }
}