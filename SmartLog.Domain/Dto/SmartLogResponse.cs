using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SmartLog.Domain.Dto
{
  [XmlRoot(nameof(SmartLogResponse))]
  public class SmartLogResponse
  {
    [XmlElement("Code")]
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [XmlElement("Error", IsNullable = false)]
    [JsonPropertyName("error")]
    public ErrorDto Error { get; set; }
  }
}
