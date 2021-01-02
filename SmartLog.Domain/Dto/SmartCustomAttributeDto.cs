using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SmartLog.Domain.Dto
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