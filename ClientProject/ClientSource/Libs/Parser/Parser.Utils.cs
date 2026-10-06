using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using System.IO;
using System.Text;


namespace CUILibs
{
  public partial class Parser
  {
    public static bool IsNullable(Type type) => Nullable.GetUnderlyingType(type) != null;
    public static object CreateDefaultFor(Type T)
    {
      if (T is null) return null;
      if (T == typeof(void)) return null;
      if (T.IsValueType) return Activator.CreateInstance(T);
      return null;
    }
  }
}
