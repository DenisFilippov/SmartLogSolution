using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SmartLog.Domain.Dto
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
    public SmartCustomAttributeDto[] CustomAttributes { get; set; }

    [XmlArray("LogDatas")]
    [XmlArrayItem("LogData")]
    [JsonPropertyName("logData")]
    public SmartLogDataDto[] LogDatas { get; set; }

    [XmlArray("Children")]
    [XmlArrayItem("Log")]
    [JsonPropertyName("log")]
    public SmartLogDto[] Children { get; set; }
  }
}