using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLog.Domain.Interfaces
{
  public interface IConnector
  {
    SqlConnection GetConnection();
  }
}