using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SmartLog.Domain.Dto
{
  [XmlRoot(nameof(SmartLogInfoResponse))]
  public class SmartLogInfoResponse
  {
    [XmlElement("Version")]
    [JsonPropertyName("version")]
    public string Version { get; set; }
  }
}
