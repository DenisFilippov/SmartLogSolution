using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartLog.Domain.Dto;

namespace SmartLog.Domain.Interfaces
{
  public interface IConfigDto
  {
    ConnectionDto Connection { get; set; }
  }
}