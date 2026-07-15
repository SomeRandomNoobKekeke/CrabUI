using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;


namespace CUILibs
{
  public class DebugEvent
  {
    public string Type { get; set; }
    public string Msg { get; set; }
    public object[] Args { get; set; }

    public override string ToString() => Msg is null ? $"{Logger.Wrap.IEnumerable(Args)}" : $"{Msg}";
  }
}