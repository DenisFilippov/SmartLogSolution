﻿using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SmartLog.Core
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