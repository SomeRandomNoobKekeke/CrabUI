using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

using ComponentGenerator;
using BaroJunk;

namespace CrabUI
{
  /// <summary>
  /// This is additional info about CUIComponent type
  /// </summary>
  public class CUIComponentInfo
  {
    public Type ComponentType { get; set; }
    public Dictionary<string, PropertyInfo> SerializableProps { get; set; }

    public override string ToString()
      => $"{ComponentType.Name}:{{\n{Logger.Wrap.IEnumerable(SerializableProps.Keys, true)}\n}}";


  }
}