using System;

namespace SmartLog.Domain.Dto
{
  public class SelectDto
  {
    public long LogsId { get; set; }

    public Guid LogGuid { get; set; }

    public long Parent { get; set; }

    public string MethodName { get; set; }

    public byte Type { get; set; }

    public DateTime CreateDate { get; set; }

    public string Message { get; set; }

    public long CustomAttributesId { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }
  }
}
