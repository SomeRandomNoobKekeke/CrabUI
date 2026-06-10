using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.Text;
using System.Runtime.CompilerServices;

#if JSON_AVAILABLE
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
#endif



namespace BaroJunk
{
  public partial class Logger
  {
    /// <summary>
    /// Some custom serialization methods,
    /// Tooks them from parser, some of them are garbage
    /// </summary>
    public class Wrap
    {
      public static string ExceptionMessage(Exception e)
        => $"[{e.Message}{(e.InnerException is null ? null : $" - {e.InnerException.Message}")}]";
      public static string IEnumerable(IEnumerable<object> array, bool newline = false)
      {
        if (newline)
        {
          return $"[\n{String.Join(",\n", array?.Select(o => $"    {WrapInColor(o?.ToString(), "white")}") ?? new string[] { })}\n]";
        }
        else
        {
          return $"[{String.Join(", ", array?.Select(o => WrapInColor(o?.ToString(), "white")) ?? new string[] { })}]";
        }
      }

      public static string IDictionary(System.Collections.IDictionary dict)
      {
        StringBuilder sb = new StringBuilder();

        sb.Append("{\n");
        foreach (System.Collections.DictionaryEntry entry in dict)
        {
          sb.Append($"    {entry.Key}: [{WrapInColor(entry.Value, "white")}],\n");
        }
        sb.Append("} ");

        return sb.ToString();
      }

      //BRUH borked
      // public static string Object(object target)
      // {
      //   StringBuilder sb = new StringBuilder();

      //   bool isPrimitive(PropertyInfo pi)
      //     => pi.PropertyType.IsPrimitive || pi.PropertyType == typeof(string);

      //   void ToStringRec(string offset, object o)
      //   {
      //     Logger.Default.Log($"Wrapping [{o}]");
      //     if (o is null)
      //     {
      //       sb.Append("[null]");
      //       return;
      //     }

      //     PropertyInfo[] props = o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

      //     foreach (PropertyInfo pi in props)
      //     {
      //       if (!isPrimitive(pi))
      //       {
      //         object value = pi.GetValue(o);

      //         sb.Append($"{offset}{pi.PropertyType.Name}  {pi.Name}:\n");
      //         ToStringRec($"{offset}       |", value);
      //         sb.Append($"{offset}        \n");
      //       }
      //     }

      //     foreach (PropertyInfo pi in props)
      //     {
      //       if (isPrimitive(pi))
      //       {
      //         sb.Append($"{offset}{pi.PropertyType.Name}  {pi.Name}: [{WrapInColor(pi.GetValue(o), "white")}]\n");
      //       }
      //     }
      //   }

      //   ToStringRec("", target);
      //   sb.Remove(sb.Length - 1, 1);
      //   return sb.ToString();
      // }

#if JSON_AVAILABLE
      public static string AsJson(object target)
      {
        return JsonSerializer.Serialize(target, new JsonSerializerOptions
        {
          WriteIndented = true,
          Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        });
      }
#endif
      /// <summary>
      /// Just direct props of an object
      /// </summary>
      public static string Props(object target)
      {
        if (target is null) return "[null]";
        StringBuilder sb = new StringBuilder();
        foreach (PropertyInfo pi in target.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
          sb.Append($"{pi.PropertyType.Name}  {pi.Name}: [{WrapInColor(pi.GetValue(target), "white")}]\n");
        }

        return sb.ToString();
      }



      public static string Vars(object arg1,
        [CallerArgumentExpression("arg1")] string exp1 = null
      )
      {
        return $"{exp1}: [{WrapInColor(arg1, "white")}]";
      }

      public static string Vars(object arg1, object arg2,
        [CallerArgumentExpression("arg1")] string exp1 = null,
        [CallerArgumentExpression("arg2")] string exp2 = null
      )
      {
        return $"{exp1}: [{WrapInColor(arg1, "white")}], {exp2}: [{WrapInColor(arg2, "white")}]";
      }


      public static string Vars(object arg1, object arg2, object arg3,
        [CallerArgumentExpression("arg1")] string exp1 = null,
        [CallerArgumentExpression("arg2")] string exp2 = null,
        [CallerArgumentExpression("arg3")] string exp3 = null
      )
      {
        return $"{exp1}: [{WrapInColor(arg1, "white")}], {exp2}: [{WrapInColor(arg2, "white")}], {exp3}: [{WrapInColor(arg3, "white")}]";
      }

      public static string Vars(object arg1, object arg2, object arg3, object arg4,
        [CallerArgumentExpression("arg1")] string exp1 = null,
        [CallerArgumentExpression("arg2")] string exp2 = null,
        [CallerArgumentExpression("arg3")] string exp3 = null,
        [CallerArgumentExpression("arg4")] string exp4 = null
      )
      {
        return $"{exp1}: [{WrapInColor(arg1, "white")}], {exp2}: [{WrapInColor(arg2, "white")}], {exp3}: [{WrapInColor(arg3, "white")}], {exp4}: [{WrapInColor(arg4, "white")}]";
      }

      public static string Vars(object arg1, object arg2, object arg3, object arg4, object arg5,
        [CallerArgumentExpression("arg1")] string exp1 = null,
        [CallerArgumentExpression("arg2")] string exp2 = null,
        [CallerArgumentExpression("arg3")] string exp3 = null,
        [CallerArgumentExpression("arg4")] string exp4 = null,
        [CallerArgumentExpression("arg5")] string exp5 = null
      )
      {
        return $"{exp1}: [{WrapInColor(arg1, "white")}], {exp2}: [{WrapInColor(arg2, "white")}], {exp3}: [{WrapInColor(arg3, "white")}], {exp4}: [{WrapInColor(arg4, "white")}], {exp5}: [{WrapInColor(arg5, "white")}]";
      }


    }
  }

}