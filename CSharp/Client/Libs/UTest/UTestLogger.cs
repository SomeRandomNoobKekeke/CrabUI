using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Barotrauma;
using Microsoft.Xna.Framework;

namespace BaroJunk
{
  // useless class that is referenced from all over the place
  public class UTestLogger
  {
    public static Color SuccessColor = Color.Lime;
    public static Color NotGreatColor = Color.Orange;
    public static Color FailureColor = Color.Red;
    public static string InnerTextColor = "128,255,255";
    public static float ColorContrast = 0.75f;
    public static string Line = "----------------------------------------------------------------------------------------";

    /// <summary>
    /// sneaky var that no one will ever find
    /// </summary>
    public static bool CollapseTestPackIfSucceed { get; set; } = true;
    public static Color UTestStateColor(bool state) => state ?
      Color.Lerp(Color.White, SuccessColor, ColorContrast) :
      Color.Lerp(Color.White, FailureColor, ColorContrast);

    public static Color UTestPackStateColor(UTestPack.UTestPackState state)
    {
      return state switch
      {
        UTestPack.UTestPackState.AllPassed => Color.Lerp(Color.White, SuccessColor, ColorContrast),
        UTestPack.UTestPackState.SomePassed => Color.Lerp(Color.White, NotGreatColor, ColorContrast),
        UTestPack.UTestPackState.AllFailed => Color.Lerp(Color.White, FailureColor, ColorContrast),
        UTestPack.UTestPackState.Error => Color.Lerp(Color.White, FailureColor, ColorContrast),
      };
    }

    public static void PrintStackTrace()
    {
      StackTrace st = new StackTrace(true);
      for (int i = 0; i < st.FrameCount; i++)
      {
        StackFrame sf = st.GetFrame(i);
        if (sf.GetMethod().DeclaringType is null)
        {
          Log($"-> {sf.GetMethod().DeclaringType?.Name}.{sf.GetMethod()}");
          break;
        }
        Log($"-> {sf.GetMethod().DeclaringType?.Name}.{sf.GetMethod()}");
      }
    }



    public static string WrapInColor(object msg, string color)
      => $"‖color:{color}‖{msg}‖end‖";

    public static string AsText(object msg) => WrapInColor(msg, InnerTextColor);

    public static void Log(object msg, Color? cl = null)
    {
      cl ??= Color.Cyan;
      LuaCsLogger.LogMessage($"{msg ?? "null"}", cl * 0.8f, cl);
    }

    public static void Warning(object msg) => Log(msg, Color.Yellow);

    public static void LogTest(UTest test)
    {
      Log(test, UTestStateColor(test.Passed));
    }

    public static void LogPack(UTestPack pack)
    {
      Color cl = UTestPackStateColor(pack.State);

      Log(Line);
      Log($"UTestPack {pack.GetType()} [{pack.PassedCount}/{pack.Tests.Count}]:", cl);

      if (pack.Error is not null)
      {
        Log($"Error: [{pack.Error.Message}]", cl);
      }

      if (!CollapseTestPackIfSucceed || pack.PassedCount != pack.Tests.Count)
      {
        foreach (UTest test in pack.Tests)
        {
          test.Log();
        }
      }

    }


  }
}