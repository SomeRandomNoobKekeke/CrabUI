using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Barotrauma;
using Microsoft.Xna.Framework;
using System.IO;
using System.Text;

namespace BaroJunk
{
  public partial class Logger
  {
    static Logger() => ProjectInfo.Add(new PackageInfo()
    {
      Name = "Logger",
      Version = new Version(0, 0, 0)
      {
        Branch = "BaroJunk",
      },
    });

    public static Logger Default = new Logger();

    public static string WrapInColor(object msg, string color) => $"‖color:{color}‖{msg}‖end‖";

    public interface ISerializer { public string Serialize(object o); }
    public class MicroSerializer : ISerializer
    {
      public string Serialize(object o)
      {
        if (o == null) return "[null]";
        if (o == "") return "[empty string]";
        return o.ToString();
      }
    }

    public Color LogColor { get; set; } = Color.Cyan;
    public Color WarningColor { get; set; } = Color.Yellow;
    public Color ErrorColor { get; set; } = Color.Red;
    public Color FunnyColor { get; set; } = Color.Magenta;

    /// <summary>
    /// Set this to true to see the source of the logs
    /// </summary>
    public bool PrintFilePath { get; set; } = false;
    public bool PrintLogs { get; set; } = true;
    public bool PrintWarnings { get; set; } = true;
    public bool PrintErrors { get; set; } = true;

    public ISerializer Serializer { get; set; } = new MicroSerializer();


    /// <summary>
    /// Log with LogColor
    /// </summary>
    public void Log(object msg, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    { if (PrintLogs) Print(msg, LogColor, source, lineNumber); }
    public void Log(object msg1, object msg2, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    {
      Log(msg1, LogColor, source, lineNumber);
      Log(msg2, LogColor, source, lineNumber);
    }
    public void Log(object msg1, object msg2, object msg3, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    {
      Log(msg1, LogColor, source, lineNumber);
      Log(msg2, LogColor, source, lineNumber);
      Log(msg3, LogColor, source, lineNumber);
    }

    /// <summary>
    /// Log with WarningColor
    /// </summary>
    /// /// 
    public void Warning(object msg, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    { if (PrintWarnings) Print(msg, WarningColor, source, lineNumber); }

    /// <summary>
    /// Log with ErrorColor
    /// </summary>
    public void Error(object msg, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    { if (PrintErrors) Print(msg, ErrorColor, source, lineNumber); }

    /// <summary>
    /// Log with Color
    /// </summary>
    public void Print(object msg, Color color, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    {
      if (PrintFilePath) _PrintFilePath(color, source, lineNumber);
      _Print(msg, color);
    }

    /// <summary>
    /// Log with file path
    /// </summary>
    public void Info(object msg, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    {
      _PrintFilePath(LogColor * 0.8f, source, lineNumber);
      _Print(msg, LogColor);
    }

    /// <summary>
    /// Print file path and line number with funny color
    /// For debuging
    /// </summary>
    public void Point([CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
      => _PrintFilePath(FunnyColor, source, lineNumber);


    /// <summary>
    /// Print stack trace
    /// For debuging
    /// </summary>
    public void PrintStackTrace()
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

    private void _Print(object msg, Color color)
    {
      LuaCsLogger.LogMessage(Serializer.Serialize(msg), color * 0.8f, color);
    }

    private void _PrintFilePath(Color color, string source, int lineNumber)
    {
      var fi = new FileInfo(source);
      LuaCsLogger.LogMessage($"{fi.Directory.Name}/{fi.Name}:{lineNumber}", color * 0.8f, color);
    }

    public void LogVars(object arg1,
      [CallerArgumentExpression("arg1")] string exp1 = null
    )
    {
      _Print($"{exp1}: [{WrapInColor(arg1, "white")}]", LogColor);
    }

    public void LogVars(object arg1, object arg2,
      [CallerArgumentExpression("arg1")] string exp1 = null,
      [CallerArgumentExpression("arg2")] string exp2 = null
    )
    {
      _Print($"{exp1}: [{WrapInColor(arg1, "white")}], {exp2}: [{WrapInColor(arg2, "white")}]", LogColor);
    }


    public void LogVars(object arg1, object arg2, object arg3,
      [CallerArgumentExpression("arg1")] string exp1 = null,
      [CallerArgumentExpression("arg2")] string exp2 = null,
      [CallerArgumentExpression("arg3")] string exp3 = null
    )
    {
      _Print($"{exp1}: [{WrapInColor(arg1, "white")}], {exp2}: [{WrapInColor(arg2, "white")}], {exp3}: [{WrapInColor(arg3, "white")}]", LogColor);
    }

    public void LogVars(object arg1, object arg2, object arg3, object arg4,
      [CallerArgumentExpression("arg1")] string exp1 = null,
      [CallerArgumentExpression("arg2")] string exp2 = null,
      [CallerArgumentExpression("arg3")] string exp3 = null,
      [CallerArgumentExpression("arg4")] string exp4 = null
    )
    {
      _Print($"{exp1}: [{WrapInColor(arg1, "white")}], {exp2}: [{WrapInColor(arg2, "white")}], {exp3}: [{WrapInColor(arg3, "white")}], {exp4}: [{WrapInColor(arg4, "white")}]", LogColor);
    }

    public void LogVars(object arg1, object arg2, object arg3, object arg4, object arg5,
      [CallerArgumentExpression("arg1")] string exp1 = null,
      [CallerArgumentExpression("arg2")] string exp2 = null,
      [CallerArgumentExpression("arg3")] string exp3 = null,
      [CallerArgumentExpression("arg4")] string exp4 = null,
      [CallerArgumentExpression("arg5")] string exp5 = null
    )
    {
      _Print($"{exp1}: [{WrapInColor(arg1, "white")}], {exp2}: [{WrapInColor(arg2, "white")}], {exp3}: [{WrapInColor(arg3, "white")}], {exp4}: [{WrapInColor(arg4, "white")}], {exp5}: [{WrapInColor(arg5, "white")}]", LogColor);
    }


  }
}
