using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using BaroJunk;

namespace CrabUI
{
  /// <summary>
  /// This is additional info about CUIComponent type
  /// </summary>
  public class CUIComponentInfo(Type T)
  {
    public Type ComponentType { get; set; } = T;
    public Dictionary<string, PropertyPath> SerializableProps { get; set; } = new();
    public ICUIStyle DefaultStyle { get; set; }
    public CUIComponent DefaultValue { get; set; }

    public override string ToString()
      => $"{ComponentType.Name}:{{\n{Logger.Wrap.IEnumerable(SerializableProps.Keys, true)}\n}}";


  }
}