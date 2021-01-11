using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SmartLog.Core
{
  public class SmartCustomAttributeDto
  {
    [XmlElement("Name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [XmlElement("Value")]
    [JsonPropertyName("value")]
    public string Value { get; set; }
  }
}