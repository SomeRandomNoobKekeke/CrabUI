using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Barotrauma;
using System.Runtime.CompilerServices;
using System.IO;

namespace BaroJunk
{

  /// <summary>
  /// Call BreakTheLoop.After(10) to break potential while(true) loop anywhere
  /// Primarily for debug
  /// </summary>
  public static class BreakTheLoop
  {
    public static Dictionary<string, int> Loops = new Dictionary<string, int>();
    public static void After(int loops, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    {
      string key = $"{source}:{lineNumber}";
      if (!Loops.ContainsKey(key)) Loops[key] = 0;
      Loops[key] += 1;

      if (Loops[key] > loops)
      {
        Loops.Remove(key);
        throw new Exception($"death loop at {Path.GetFileName(source)}:{lineNumber}");
      }
    }
  }
}