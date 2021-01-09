using Dapper.FluentMap.Mapping;
using SmartLog.DAL.Entities;

namespace SmartLog.DAL.Map
{
  public class SelectEntityMap : EntityMap<SelectEntity>
  {
    public SelectEntityMap()
    {
      Map(r => r.LogsId).ToColumn("logs_id");
      Map(r => r.LogGuid).ToColumn("log_guid");
      Map(r => r.Parent).ToColumn("parent");
      Map(r => r.MethodName).ToColumn("method_name");
      Map(r => r.Type).ToColumn("typet");
      Map(r => r.CreateDate).ToColumn("create_date");
      Map(r => r.Message).ToColumn("message");
      Map(r => r.CustomAttributesId).ToColumn("custom_attributes_id");
      Map(r => r.Name).ToColumn("name");
      Map(r => r.Value).ToColumn("value");
    }
  }
}