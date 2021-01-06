using System;
using System.Runtime.Serialization;

namespace SmartLog.Domain
{
  [Serializable]
  public class CreateLogException : Exception
  {
    public CreateLogException(Guid? uid)
    {
      UId = uid;
    }

    public CreateLogException(Guid? uid, string message) : base(message)
    {
      UId = uid;
    }

    public CreateLogException(Guid? uid, string message, Exception inner) : base(message, inner)
    {
      UId = uid;
    }

    protected CreateLogException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public Guid? UId { get; private set; }
  }
}
