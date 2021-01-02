using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SmartLog.Domain.Dto
{
  public class ErrorDto
  {
    [XmlElement("UId")]
    [JsonPropertyName("uid")]
    public Guid UId { get; set; }

    [XmlElement("Message")]
    [JsonPropertyName("message")]
    public string Message { get; set; }
  }
}