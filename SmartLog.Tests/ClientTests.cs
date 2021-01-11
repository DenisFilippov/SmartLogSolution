using System;
using NUnit.Framework;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SmartLog.Client;
using SmartLog.Core;

namespace SmartLog.Tests
{
  public class ClientTests
  {
    private Uri _serverUrl;

    private LogClient Create()
    {
      var result = new LogClient(_serverUrl);
      result.ConnectionLeaseTimeout = 0;
      return result;
    }

    private T Deserialize<T>(string s)
    {
      var serializer = new XmlSerializer(typeof(T));
      using var reader = new StringReader(s);
      return (T)serializer.Deserialize(reader);
    }

    [SetUp]
    public void Setup()
    {
      _serverUrl = new Uri("http://localhost:53946/");
    }

    [Test]
    public async Task GetLogInfoTestAsync()
    {
      var logClient = Create();
      var result = await logClient.GetLogInfoAsync();
      Assert.Pass("Тест пройден.");
    }

    [Test]
    public async Task GetDateByRangeLogTestAsync()
    {
      var logClient = Create();
      var result = await logClient.GetDateByRangeAsync(new DateTime(2021, 1, 1, 21, 0, 0), new DateTime(2021, 1, 1, 21, 3, 0));
      Assert.Pass("Тест пройден.");
    }

    [Test]
    public async Task CreateLogTestAsync()
    {
      var logClient = Create();
      var request = Deserialize<SmartLogRequest>(text.insert_request);
      var result = await logClient.CreateLogAsync(request);
      Assert.Pass("Тест пройден.");
    }
  }
}