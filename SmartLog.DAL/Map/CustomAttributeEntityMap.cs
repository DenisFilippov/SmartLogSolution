using Dapper.FluentMap.Mapping;
using SmartLog.DAL.Entities;

namespace SmartLog.DAL.Map
{
  public class CustomAttributeEntityMap : EntityMap<CustomAttributeEntity>
  {
    public CustomAttributeEntityMap()
    {
      Map(r => r.Id).ToColumn("custom_attributes_id");
      Map(r => r.LogsId).ToColumn("logs_id");
      Map(r => r.Name).ToColumn("name");
      Map(r => r.Value).ToColumn("value");
    }
  }
}
