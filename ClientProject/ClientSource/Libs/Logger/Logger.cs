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

namespace CUILibs
{
  public partial class Logger
  {
    public static Logger Default = new Logger() { PrintFilePath = false };

#if CLIENT
    public static string WrapInColor(object msg, string color) => $"‖color:{color}‖{msg}‖end‖";
    public static string White(object msg) => $"‖color:white‖{msg}‖end‖";
#elif SERVER
    public static string WrapInColor(object msg, string color) => msg.ToString();
    public static string White(object msg) => msg.ToString();
#endif


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


    public ISerializer Serializer { get; set; } = new MicroSerializer();


    /// <summary>
    /// Log with LogColor
    /// </summary>
    public void Log(object msg1)
    {
      Print(msg1, LogColor);
    }
    public void Log(object msg1, object msg2)
    {
      Print(msg1, LogColor);
      Print(msg2, LogColor);
    }
    public void Log(object msg1, object msg2, object msg3)
    {
      Print(msg1, LogColor);
      Print(msg2, LogColor);
      Print(msg3, LogColor);
    }

    public void Log(object msg1, object msg2, object msg3, object msg4)
    {
      Print(msg1, LogColor);
      Print(msg2, LogColor);
      Print(msg3, LogColor);
      Print(msg4, LogColor);
    }

    public void Log(object msg1, object msg2, object msg3, object msg4, object msg5)
    {
      Print(msg1, LogColor);
      Print(msg2, LogColor);
      Print(msg3, LogColor);
      Print(msg4, LogColor);
      Print(msg5, LogColor);
    }

    public void Warning(object msg) => Print(msg, WarningColor);
    public void Error(object msg) => Print(msg, ErrorColor);

    /// <summary>
    /// Log with Color
    /// </summary>
    public void Print(object msg, Color color)
    {
#if CLIENT
      DebugConsole.NewMessage(Serializer.Serialize(msg), color);
#else
      DebugConsole.NewMessage(Serializer.Serialize(msg), color * 0.8f);
#endif
    }

    /// <summary>
    /// Log with file path
    /// Use this for print debugging, because in case you forget to remove it
    /// you will have option to turn PrintFilePath to true and see exact file and line number where you left it 
    /// </summary>
    public void Info(object msg, [CallerFilePath] string source = "", [CallerLineNumber] int lineNumber = 0)
    {
      if (PrintFilePath) _PrintFilePath(LogColor * 0.8f, source, lineNumber);
      Print(msg, LogColor);
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
        Log($"-> {sf.GetMethod().DeclaringType?.Name}.{sf.GetMethod()}");
      }
      Log($"\n");
    }

    private void _PrintFilePath(Color color, string source, int lineNumber)
    {
      var fi = new FileInfo(source);
#if CLIENT
      DebugConsole.NewMessage($"{fi.Directory.Name}/{fi.Name}:{lineNumber}", color);
#else
      DebugConsole.NewMessage($"{fi.Directory.Name}/{fi.Name}:{lineNumber}", color * 0.8f);
#endif
    }

    public void LogVars(object arg1,
      [CallerArgumentExpression("arg1")] string exp1 = null
    )
    {
      Print($"{exp1}: [{WrapInColor(arg1, "white")}]", LogColor);
    }

    public void LogVars(object arg1, object arg2,
      [CallerArgumentExpression("arg1")] string exp1 = null,
      [CallerArgumentExpression("arg2")] string exp2 = null
    )
    {
      Print($"{exp1}: [{WrapInColor(arg1, "white")}], {exp2}: [{WrapInColor(arg2, "white")}]", LogColor);
    }


    public void LogVars(object arg1, object arg2, object arg3,
      [CallerArgumentExpression("arg1")] string exp1 = null,
      [CallerArgumentExpression("arg2")] string exp2 = null,
      [CallerArgumentExpression("arg3")] string exp3 = null
    )
    {
      Print($"{exp1}: [{WrapInColor(arg1, "white")}], {exp2}: [{WrapInColor(arg2, "white")}], {exp3}: [{WrapInColor(arg3, "white")}]", LogColor);
    }

    public void LogVars(object arg1, object arg2, object arg3, object arg4,
      [CallerArgumentExpression("arg1")] string exp1 = null,
      [CallerArgumentExpression("arg2")] string exp2 = null,
      [CallerArgumentExpression("arg3")] string exp3 = null,
      [CallerArgumentExpression("arg4")] string exp4 = null
    )
    {
      Print($"{exp1}: [{WrapInColor(arg1, "white")}], {exp2}: [{WrapInColor(arg2, "white")}], {exp3}: [{WrapInColor(arg3, "white")}], {exp4}: [{WrapInColor(arg4, "white")}]", LogColor);
    }

    public void LogVars(object arg1, object arg2, object arg3, object arg4, object arg5,
      [CallerArgumentExpression("arg1")] string exp1 = null,
      [CallerArgumentExpression("arg2")] string exp2 = null,
      [CallerArgumentExpression("arg3")] string exp3 = null,
      [CallerArgumentExpression("arg4")] string exp4 = null,
      [CallerArgumentExpression("arg5")] string exp5 = null
    )
    {
      Print($"{exp1}: [{WrapInColor(arg1, "white")}], {exp2}: [{WrapInColor(arg2, "white")}], {exp3}: [{WrapInColor(arg3, "white")}], {exp4}: [{WrapInColor(arg4, "white")}], {exp5}: [{WrapInColor(arg5, "white")}]", LogColor);
    }


  }
}
