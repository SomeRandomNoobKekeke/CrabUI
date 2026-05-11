using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;


namespace BaroJunk
{
  public class DebugEvent
  {
    public string Name { get; set; }
    public string Msg { get; set; }
    public object[] Args { get; set; }

    public override string ToString() => Msg is null ?
      $"{Name}| {Logger.Wrap.IEnumerable(Args)}" :
      $"{Name}| {Msg}";
  }
}