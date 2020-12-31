namespace SmartLog.Domain.Dto
{
  public class LogDataDto
  {
    public long Id { get; set; }

    public long LogsId { get; set; }

    public string Key { get; set; }

    public byte[] Value { get; set; }
  }
}