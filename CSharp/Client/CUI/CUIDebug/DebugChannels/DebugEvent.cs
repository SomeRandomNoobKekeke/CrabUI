using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;

namespace CrabUI
{
  public class DebugEvent
  {
    public string Msg { get; set; }
    public object[] Args { get; set; }

    private string DefaultToString() => Logger.Wrap.IEnumerable(Args);

    public override string ToString()
      => Msg is null ? DefaultToString() : Msg;
  }
}