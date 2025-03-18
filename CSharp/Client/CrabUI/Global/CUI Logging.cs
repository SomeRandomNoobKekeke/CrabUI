using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.IO;

namespace CrabUI
{
  public partial class CUI
  {
    /// <summary>
    /// $"‖color:{color}‖{msg}‖end‖"
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string WrapInColor(object msg, string color)
    {
      return $"‖color:{color}‖{msg}‖end‖";
    }

    //HACK too lazy to make good name
    /// <summary>
    /// Serializes the array
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static string ArrayToString(IEnumerable<object> array)
    {
      return $"[{String.Join(", ", array.Select(o => o.ToString()))}]";
    }

    /// <summary>
    /// Prints a message to console
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="color"></param>
    public static void Log(object msg, Color? color = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    {
      color ??= Color.Cyan;

      // var fi = new FileInfo(source);
      // LuaCsLogger.LogMessage($"{fi.Directory.Name}/{fi.Name}:{lineNumber}", color * 0.6f, color * 0.6f);

      LuaCsLogger.LogMessage($"{msg ?? "null"}", color * 0.8f, color);
    }

    public static void Warning(object msg, Color? color = null, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    {
      color ??= Color.Yellow;
      // var fi = new FileInfo(source);
      // LuaCsLogger.LogMessage($"{fi.Directory.Name}/{fi.Name}:{lineNumber}", color * 0.6f, color * 0.6f);
      LuaCsLogger.LogMessage($"{msg ?? "null"}", color * 0.8f, color);
    }


    /// <summary>
    /// xd
    /// </summary>
    /// <param name="source"> This should be injected by compiler, don't set </param>
    /// <returns></returns>
    public static string GetCallerFolderPath([CallerFilePath] string source = "") => Path.GetDirectoryName(source);

    /// <summary>
    /// Prints debug message with source path
    /// Works only if debug is true
    /// </summary>
    /// <param name="msg"></param>
    public static void Info(object msg, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    {
      if (Debug == true)
      {
        var fi = new FileInfo(source);

        Log($"{fi.Directory.Name}/{fi.Name}:{lineNumber}", Color.Yellow * 0.5f);
        Log(msg, Color.Yellow);
      }
    }
  }
}
