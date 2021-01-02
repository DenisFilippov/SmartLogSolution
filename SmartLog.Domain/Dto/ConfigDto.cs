using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartLog.Domain.Interfaces;

namespace SmartLog.Domain.Dto
{
  public class ConfigDto : IConfigDto
  {
    public ConnectionDto Connection { get; set; }
  }
}
