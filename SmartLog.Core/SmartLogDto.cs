using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SmartLog.Core
{
  public class SmartLogDto
  {
    [XmlElement("UId")]
    [JsonPropertyName("uid")]
    public Guid UId { get; set; }

    [XmlElement("MethodName")]
    [JsonPropertyName("methodName")]
    public string MethodName { get; set; }

    [XmlElement("Type")]
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [XmlElement("CreateDate")]
    [JsonPropertyName("createDate")]
    public DateTime CreateDate { get; set; }

    [XmlElement("Message")]
    [JsonPropertyName("message")]
    public string Message { get; set; }

    [XmlArray("CustomAttributes")]
    [XmlArrayItem("CustomAttribute")]
    [JsonPropertyName("customAttribute")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SmartCustomAttributeDto[] CustomAttributes { get; set; }

    [XmlArray("Children")]
    [XmlArrayItem("Log")]
    [JsonPropertyName("log")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SmartLogDto[] Children { get; set; }
  }
}