namespace SmartLog.Domain.Dto
{
  public class CustomAttributeDto
  {
    public long Id { get; set; }

    public long LogsId { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }
  }
}