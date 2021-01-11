using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SmartLog.Core;

namespace SmartLog.Client
{
  public class LogClient
  {
    private readonly Uri _serverUri;

    private async Task<string> SendAsync(string request, string tail, HttpMethod method)
    {
      using var handler = new SocketsHttpHandler
      {
        PooledConnectionLifetime = TimeSpan.FromMilliseconds(ConnectionLeaseTimeout)
      };
      using var http = new HttpClient(handler);
      using var message = new HttpRequestMessage(method, $"{_serverUri}{tail}");
      if (method == HttpMethod.Post)
      {
        message.Content = new StringContent(request, Encoding.UTF8, "application/xml");
      }

      message.Headers.Accept.ParseAdd("application/xml");

      var response = await http.SendAsync(message);
      if (!response.IsSuccessStatusCode)
        throw new InvalidOperationException(); // TODO :

      return await response.Content.ReadAsStringAsync();
    }

    public int ConnectionLimit
    {
      get => ServicePointManager.FindServicePoint(_serverUri).ConnectionLimit;
      set => ServicePointManager.FindServicePoint(_serverUri).ConnectionLimit = value;
    }

    public int ConnectionLeaseTimeout
    {
      get => ServicePointManager.FindServicePoint(_serverUri).ConnectionLeaseTimeout;
      set => ServicePointManager.FindServicePoint(_serverUri).ConnectionLeaseTimeout = value;
    }

    private static string Serialize<T>(T obj)
    {
      var serializer = new XmlSerializer(typeof(T));
      var sb = new StringBuilder(1024);
      using var writer = new Utf8Writer(sb);
      serializer.Serialize(writer, obj);
      writer.FlushAsync();
      return sb.ToString();
    }

    private static T Deserialize<T>(string s)
    {
      var serializer = new XmlSerializer(typeof(T));
      using var reader = new StringReader(s);
      return (T) serializer.Deserialize(reader);
    }

    public LogClient(Uri serverUrl)
    {
      _serverUri = serverUrl ?? throw new ArgumentNullException(nameof(serverUrl));
    }

    public async Task<SmartLogInfoResponse> GetLogInfoAsync()
    {
      const string TAIL = "log/service-info";

      var response = await SendAsync(string.Empty, TAIL, HttpMethod.Get);
      return Deserialize<SmartLogInfoResponse>(response);
    }

    public async Task<SmartLogSelectResponse> GetDateByRangeAsync(DateTime initial, DateTime final)
    {
      const string TAIL = "log/by-date-range?initial={0}&final={1}";
      const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:mm";

      var response = await SendAsync(string.Empty,
        string.Format(TAIL, initial.ToString(DATE_TIME_FORMAT), final.ToString(DATE_TIME_FORMAT)),
        HttpMethod.Get);
      return Deserialize<SmartLogSelectResponse>(response);
    }

    public async Task<SmartLogResponse> CreateLogAsync(SmartLogRequest request)
    {
      const string TAIL = "log";

      var sRequest = Serialize(request);
      var response = await SendAsync(sRequest, TAIL, HttpMethod.Post);
      return Deserialize<SmartLogResponse>(response);
    }
  }
}