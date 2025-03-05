#define CUIDEBUG
// #define SHOWPERF

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

using Barotrauma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace CrabUI
{
  public static class CUIDebug
  {
    public static bool PrintKeys;

#if !CUIDEBUG
    [Conditional("DONT")]
#endif
    public static void Log(object msg, Color? cl = null)
    {
      if (!CUI.Debug) return;
      cl ??= Color.Yellow;
      LuaCsLogger.LogMessage($"{msg ?? "null"}", cl * 0.8f, cl);
    }


#if !CUIDEBUG
    [Conditional("DONT")]
#endif
    public static void Info(object msg, Color? cl = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    {
      if (!CUI.Debug) return;
      cl ??= Color.Cyan;
      var fi = new FileInfo(source);

      CUI.Log($"{fi.Directory.Name}/{fi.Name}:{lineNumber}", cl * 0.5f);
      CUI.Log(msg, cl);
    }

#if !CUIDEBUG
    [Conditional("DONT")]
#endif
    public static void Error(object msg, Color? cl = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    {
      if (!CUI.Debug) return;
      cl ??= Color.Orange;
      var fi = new FileInfo(source);

      CUI.Log($"{fi.Directory.Name}/{fi.Name}:{lineNumber}", cl * 0.5f);
      CUI.Log(msg, cl);
    }

#if !CUIDEBUG
    [Conditional("DONT")]
#endif
    public static void Capture(CUIComponent host, CUIComponent target, string method, string sprop, string tprop, string value)
    {
      if (target == null || target.IgnoreDebug || !target.Debug) return;

      //CUI.Log($"{host} {target} {method} {sprop} {tprop} {value}");

      CUIDebugWindow.Main?.Capture(new CUIDebugEvent(host, target, method, sprop, tprop, value));
    }

#if !CUIDEBUG
    [Conditional("DONT")]
#endif
    public static void Flush() => CUIDebugWindow.Main?.Flush();


    //     public static int CUIShowperfCategory = 1000;
    // #if (!SHOWPERF || !CUIDEBUG)
    //     [Conditional("DONT")]
    // #endif
    //     public static void CaptureTicks(double ticks, string name, int hash) => ShowPerfExtensions.Plugin.CaptureTicks(ticks, CUIShowperfCategory, name, hash);

    // #if (!SHOWPERF || !CUIDEBUG)
    //     [Conditional("DONT")]
    // #endif
    //     public static void CaptureTicks(double ticks, string name) => ShowPerfExtensions.Plugin.CaptureTicks(ticks, CUIShowperfCategory, name);

    // #if (!SHOWPERF || !CUIDEBUG)
    //     [Conditional("DONT")]
    // #endif
    //     public static void EnsureCategory() => ShowPerfExtensions.Plugin.EnsureCategory(CUIShowperfCategory);
  }
}