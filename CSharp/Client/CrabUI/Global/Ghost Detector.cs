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
  public class GhostDetector
  {
    public static bool Dead { get; set; }
    public static Action<string> OnDetect;

    /// <summary>
    /// Call this in your harmony patches
    /// </summary>
    public static bool Check(
      [CallerMemberName] string memberName = "",
      [CallerFilePath] string source = "",
      [CallerLineNumber] int lineNumber = 0
    )
    {
      if (Dead)
      {
        string at = $"{CutFilePath(source)}:{lineNumber} {memberName}";

        if (ShouldReport(at)) OnDetect?.Invoke(at);
      }

      return Dead;
    }

    public static string CutFilePath(string path)
    {
      int i = path.IndexOf("CSharp");
      if (i == -1) i = path.IndexOf("SharedSource");
      if (i == -1) i = path.IndexOf("ClientSource");
      if (i == -1) i = path.IndexOf("ServerSource");

      if (i == -1) return path;
      return path.Substring(i);
    }


    public static int MinDetections = 3;
    public static int MaxDetections = 6;
    public static Dictionary<string, int> Detections = new();
    public static bool ShouldReport(string at)
    {
      if (!Detections.ContainsKey(at)) Detections[at] = 1;
      else Detections[at]++;

      return MinDetections < Detections[at] && Detections[at] <= MaxDetections;
    }

    /// <summary>
    /// Call this in IAssemblyPlugin.Initialize
    /// </summary>
    public static void Activate()
    {
      OnDetect ??= (at) =>
      {
        Log($"{CUI.HookIdentifier} GUI just died, It seems that there is a mod conflict");
        Log($"At {at}", Color.Orange);
      };
    }

    public static void Deactivate()
    {
      Dead = true;
    }

    public static void Log(object msg, Color? color = null)
    {
      color ??= Color.Yellow;
      LuaCsLogger.LogMessage($"{msg ?? "null"}", color * 0.8f, color);
    }
  }

}
