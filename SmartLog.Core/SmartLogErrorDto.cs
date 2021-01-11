using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SmartLog.Core
{
  public class SmartLogErrorDto
  {
    [XmlElement("UId")]
    [JsonPropertyName("uid")]
    public Guid UId { get; set; }

    [XmlElement("Message")]
    [JsonPropertyName("message")]
    public string Message { get; set; }
  }
}