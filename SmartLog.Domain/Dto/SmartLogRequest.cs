using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SmartLog.Domain.Dto
{
  [XmlRoot(nameof(SmartLogRequest))]
  public class SmartLogRequest
  {
    [XmlArray("Logs")]
    [XmlArrayItem("Log")]
    [JsonPropertyName("log")]
    public SmartLogDto[] Logs { get; set; }
  }
}