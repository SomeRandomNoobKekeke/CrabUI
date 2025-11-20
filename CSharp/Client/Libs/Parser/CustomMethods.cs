using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Barotrauma;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System.Text;

namespace BaroJunk
{

  public class CustomSerializeMethods
  {
    public string WrapInColor(object msg, string color) => $"‖color:{color}‖{msg}‖end‖";

    public string ExceptionMessage(Exception e)
      => $"[{e.Message}{(e.InnerException is null ? null : $" - {e.InnerException.Message}")}]";
    public string IEnumerableToString(IEnumerable<object> array)
      => $"[{String.Join(", ", array?.Select(o => o?.ToString()) ?? new string[] { })}]";

    public string IDictionaryToString(System.Collections.IDictionary dict)
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

    /// <summary>
    /// beware of loops
    /// </summary>
    public string ObjectToString(object target)
    {
      StringBuilder sb = new StringBuilder();

      void ToStringRec(string offset, object o)
      {
        if (o is null)
        {
          sb.Append("[null]");
          return;
        }

        PropertyInfo[] props = o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

        foreach (PropertyInfo pi in props)
        {
          if (!pi.PropertyType.IsPrimitive)
          {
            object value = pi.GetValue(o);

            sb.Append($"{offset}{pi.PropertyType.Name}  {pi.Name}:\n");
            ToStringRec($"{offset}       |", value);
            sb.Append($"{offset}        \n");
          }
        }

        foreach (PropertyInfo pi in props)
        {
          if (pi.PropertyType.IsPrimitive)
          {
            sb.Append($"{offset}{pi.PropertyType.Name}  {pi.Name}: [{WrapInColor(pi.GetValue(o), "white")}]\n");
          }
        }
      }

      ToStringRec("", target);
      sb.Remove(sb.Length - 1, 1);
      return sb.ToString();
    }

    /// <summary>
    /// Just direct props of an object
    /// </summary>
    public string PropsToString(object target)
    {
      if (target is null) return "[null]";
      StringBuilder sb = new StringBuilder();
      foreach (PropertyInfo pi in target.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
      {
        sb.Append($"{pi.PropertyType.Name}  {pi.Name}: [{WrapInColor(pi.GetValue(target), "white")}]\n");
      }

      return sb.ToString();
    }


  }
}