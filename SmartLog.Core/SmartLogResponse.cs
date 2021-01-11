using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SmartLog.Core
{
  [XmlRoot(nameof(SmartLogResponse))]
  public class SmartLogResponse
  {
    [XmlElement("Code")]
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [XmlElement("SmartLogError", IsNullable = false)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SmartLogErrorDto SmartLogError { get; set; }
  }
}