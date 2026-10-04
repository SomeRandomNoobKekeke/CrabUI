using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace CUILibs
{
  public static class DebugContext
  {
    public static HashSet<string> CurrentContext { get; } = new();

    public static bool IsInside(string context)
    {
      return CurrentContext.Contains(context);
    }

    public static void Enter(string context)
    {
      CurrentContext.Add(context);
    }

    public static void Exit(string context)
    {
      CurrentContext.Remove(context);
    }

    public static void Set(string context, bool state)
    {
      if (state) CurrentContext.Add(context);
      else CurrentContext.Remove(context);
    }

    public static void Log(object msg, string context)
    {
      if (IsInside(context)) Logger.Default.Log(msg);
    }
  }
}