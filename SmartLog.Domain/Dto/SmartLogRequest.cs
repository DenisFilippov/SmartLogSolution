using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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