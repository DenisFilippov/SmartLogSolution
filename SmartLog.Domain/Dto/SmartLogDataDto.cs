using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SmartLog.Domain.Dto
{
  public class SmartLogDataDto
  {
    [XmlElement("Key")]
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [XmlElement("Value")]
    [JsonPropertyName("value")]
    public byte[] Value { get; set; }
  }
}
