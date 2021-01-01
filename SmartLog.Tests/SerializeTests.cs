using AutoMapper;
using Dapper.FluentMap;
using NUnit.Framework;
using SmartLog.DAL.Map;
using SmartLog.DAL.Repository;
using SmartLog.Domain;
using SmartLog.Domain.Dto;
using SmartLog.Domain.Interfaces;
using SmartLog.Mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SmartLog.Tests
{
  public class SerializeTests
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task XmkInsertRequestTest()
    {
      var xmlSerializer = new XmlSerializer(typeof(SmartLogRequest));
      using var reader1 = new StringReader(text.insert_request);
      var requestObject1 = (SmartLogRequest) xmlSerializer.Deserialize(reader1);
      var sb = new StringBuilder(4096);
      var writer = new StringWriter(sb);
      xmlSerializer.Serialize(writer, requestObject1);
      var serialized = sb.ToString();
      using var reader2 = new StringReader(serialized);
      var requestObject2 = (SmartLogRequest) xmlSerializer.Deserialize(reader2);
      Assert.Pass("Тест пройден.");
    }

    [Test]
    public async Task JsonInsertRequestTest()
    {
      var xmlSerializer = new XmlSerializer(typeof(SmartLogRequest));
      using var reader1 = new StringReader(text.insert_request);
      var requestObject1 = (SmartLogRequest) xmlSerializer.Deserialize(reader1);

      var jsonSerializer = new JsonSerializer() {Formatting = Formatting.Indented};
      var sb = new StringBuilder(4096);
      var writer = new StringWriter(sb);
      jsonSerializer.Serialize(writer, requestObject1);
      var serialized = sb.ToString();
      using var reader2 = new StringReader(serialized);
      var requestObject2 = (SmartLogRequest) jsonSerializer.Deserialize(reader2, typeof(SmartLogRequest));

      Assert.Pass("Тест пройден.");
    }
  }
}