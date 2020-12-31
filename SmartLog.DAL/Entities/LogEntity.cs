using System;

namespace SmartLog.DAL.Entities
{
  public class LogEntity
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