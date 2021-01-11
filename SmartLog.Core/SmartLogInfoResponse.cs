using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SmartLog.Core
{
  [XmlRoot(nameof(SmartLogInfoResponse))]
  public class SmartLogInfoResponse
  {
    [XmlElement("Version")]
    [JsonPropertyName("version")]
    public string Version { get; set; }
  }
}