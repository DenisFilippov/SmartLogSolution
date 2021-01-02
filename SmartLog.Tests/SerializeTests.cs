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
    public void XmlInsertRequestTest()
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
    public void JsonInsertRequestTest()
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

    [Test]
    public void XmlResponse200Test()
    {
      var xmlSerializer = new XmlSerializer(typeof(SmartLogResponse));
      using var reader1 = new StringReader(text.response_200);
      var responseObject1 = (SmartLogResponse)xmlSerializer.Deserialize(reader1);
      var sb = new StringBuilder(4096);
      var writer = new StringWriter(sb);
      xmlSerializer.Serialize(writer, responseObject1);
      var serialized = sb.ToString();
      using var reader2 = new StringReader(serialized);
      var responseObject2 = (SmartLogResponse)xmlSerializer.Deserialize(reader2);
      Assert.Pass("Тест пройден.");
    }

    [Test]
    public void XmlResponse404Test()
    {
      var xmlSerializer = new XmlSerializer(typeof(SmartLogResponse));
      using var reader1 = new StringReader(text.response_404);
      var responseObject1 = (SmartLogResponse)xmlSerializer.Deserialize(reader1);
      var sb = new StringBuilder(4096);
      var writer = new StringWriter(sb);
      xmlSerializer.Serialize(writer, responseObject1);
      var serialized = sb.ToString();
      using var reader2 = new StringReader(serialized);
      var reponseObject2 = (SmartLogResponse)xmlSerializer.Deserialize(reader2);
      Assert.Pass("Тест пройден.");
    }

    [Test]
    public void XmlResponseInfoTest()
    {
      var xmlSerializer = new XmlSerializer(typeof(SmartLogInfoResponse));
      using var reader1 = new StringReader(text.response_info);
      var responseObject1 = (SmartLogInfoResponse)xmlSerializer.Deserialize(reader1);
      var sb = new StringBuilder(4096);
      var writer = new StringWriter(sb);
      xmlSerializer.Serialize(writer, responseObject1);
      var serialized = sb.ToString();
      using var reader2 = new StringReader(serialized);
      var responseObject2 = (SmartLogInfoResponse)xmlSerializer.Deserialize(reader2);
      Assert.Pass("Тест пройден.");
    }

    [Test]
    public void JsonResponse200Test()
    {
      var xmlSerializer = new XmlSerializer(typeof(SmartLogResponse));
      using var reader1 = new StringReader(text.response_200);
      var responseObject1 = (SmartLogResponse)xmlSerializer.Deserialize(reader1);

      var jsonSerializer = new JsonSerializer() { Formatting = Formatting.Indented };
      var sb = new StringBuilder(4096);
      var writer = new StringWriter(sb);
      jsonSerializer.Serialize(writer, responseObject1);
      var serialized = sb.ToString();
      using var reader2 = new StringReader(serialized);
      var responseObject2 = (SmartLogResponse)jsonSerializer.Deserialize(reader2, typeof(SmartLogResponse));

      Assert.Pass("Тест пройден.");
    }

    [Test]
    public void JsonResponse404Test()
    {
      var xmlSerializer = new XmlSerializer(typeof(SmartLogResponse));
      using var reader1 = new StringReader(text.response_404);
      var responseObject1 = (SmartLogResponse)xmlSerializer.Deserialize(reader1);

      var jsonSerializer = new JsonSerializer() { Formatting = Formatting.Indented };
      var sb = new StringBuilder(4096);
      var writer = new StringWriter(sb);
      jsonSerializer.Serialize(writer, responseObject1);
      var serialized = sb.ToString();
      using var reader2 = new StringReader(serialized);
      var responseObject2 = (SmartLogResponse)jsonSerializer.Deserialize(reader2, typeof(SmartLogResponse));

      Assert.Pass("Тест пройден.");
    }

    public void JsonResponseInfoTest()
    {
      var xmlSerializer = new XmlSerializer(typeof(SmartLogInfoResponse));
      using var reader1 = new StringReader(text.response_200);
      var responseObject1 = (SmartLogInfoResponse)xmlSerializer.Deserialize(reader1);

      var jsonSerializer = new JsonSerializer() { Formatting = Formatting.Indented };
      var sb = new StringBuilder(4096);
      var writer = new StringWriter(sb);
      jsonSerializer.Serialize(writer, responseObject1);
      var serialized = sb.ToString();
      using var reader2 = new StringReader(serialized);
      var responseObject2 = (SmartLogInfoResponse)jsonSerializer.Deserialize(reader2, typeof(SmartLogInfoResponse));

      Assert.Pass("Тест пройден.");
    }
  }
}