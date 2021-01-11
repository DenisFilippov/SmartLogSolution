using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLog.Client
{
  internal class Utf8Writer : StringWriter
  {
    public Utf8Writer(StringBuilder sb) : base(sb) { }

    public override Encoding Encoding { get; } = Encoding.UTF8;
  }
}
