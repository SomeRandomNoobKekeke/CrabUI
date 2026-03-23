using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using BaroJunk;


namespace CrabUI
{
  public class DebugEvent(object[] args, string name, string text = null)
  {
    public string Name { get; set; } = name;
    public object[] Args { get; } = args;
    public string Text { get; } = text;

    public override string ToString()
      => $"{Name} >> {(Text is null ? $"{Logger.Wrap.IEnumerable(Args)}" : Text)}";
  }
}