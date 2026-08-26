using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using CUICodeGenerator;
using CUILibs;

namespace CursedUI
{
  /// <summary>
  /// This is additional info about CUIVisualComponent type
  /// </summary>
  public class CUIVisualComponentInfo
  {
    public Type ComponentType { get; set; }
    public Dictionary<string, PropertyPath> SerializableProps { get; set; } = new();
    public ICUIStyle? DefaultStyle { get; set; }
    public CUIVisualComponent DefaultValue { get; set; }

    public override string ToString()
      => $"{ComponentType.Name}:{{\n{Logger.Wrap.IEnumerable(SerializableProps.Keys, true)}\n}}";


  }
}