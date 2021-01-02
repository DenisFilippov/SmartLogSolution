using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartLog.Domain.Interfaces;

namespace SmartLog.Domain
{
  public class Connector : IConnector
  {
    private readonly string _connectionString;

    public Connector(string connectionString)
    {
      _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public SqlConnection GetConnection()
    {
      var result = new SqlConnection(_connectionString);
      result.Open();
      return result;
    }
  }
}