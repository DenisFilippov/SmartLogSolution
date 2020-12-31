using System;
using System.Collections.Generic;

namespace SmartLog.Domain.Dto
{
  public class LogDto
  {
    public long Id { get; set; }

    public Guid Uid { get; set; }

    public DateTime CreateDate { get; set; }

    public string Message { get; set; }

    public long Parent { get; set; }

    public byte Type { get; set; }

    public string MethodName { get; set; }
  }
}