using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SmartLog.Domain.Dto
{
  [XmlRoot(nameof(SmartLogSelectResponse))]
  public class SmartLogSelectResponse : SmartLogResponse
  {
    [XmlArray("Logs")]
    [XmlArrayItem("Log", IsNullable = false)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SmartLogDto[] Logs { get; set; }
  }
}