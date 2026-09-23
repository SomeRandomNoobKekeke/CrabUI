using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Barotrauma;
using Microsoft.Xna.Framework;
using System.Text.Json;

namespace CUILibs
{
  public static class DeepDictAccess
  {

    public static void Print(IDictionary<string, object> dict)
    {
      Logger.Default.Log(JsonSerializer.Serialize(dict, options: new JsonSerializerOptions()
      {
        WriteIndented = true,
      }));
    }

    public static bool Has(string path, IDictionary<string, object> dict)
    {
      return TryGetValue(path, dict, out object _);
    }

    public static void Set(string path, object value, IDictionary<string, object> dict)
    {
      if (string.IsNullOrEmpty(path)) return;

      string[] parts = path.Split('.');

      foreach (string part in parts.SkipLast(1))
      {
        if (dict.TryGetValue(part, out object o) && o is IDictionary<string, object>)
        {
          dict = o as IDictionary<string, object>;
        }
        else
        {
          Dictionary<string, object> nested = new();
          dict[part] = nested;
          dict = nested;
        }
      }

      dict[parts.Last()] = value;
    }


    public static T Get<T>(string path, IDictionary<string, object> dict)
    {
      if (TryGetValue(path, dict, out object result))
      {
        if (result.GetType().IsAssignableTo(typeof(T)))
        {
          return (T)result;
        }
      }

      return default;
    }

    public static object Get(string path, IDictionary<string, object> dict)
    {
      TryGetValue(path, dict, out object result);
      return result;
    }


    public static bool TryGetValue(string path, IDictionary<string, object> dict, out object result)
    {
      if (string.IsNullOrEmpty(path))
      {
        result = null;
        return false;
      }

      string[] parts = path.Split('.');
      object o = dict;

      foreach (string part in parts)
      {
        if (o is not IDictionary<string, object>)
        {
          result = null;
          return false;
        }

        if (((IDictionary<string, object>)o).TryGetValue(part, out object next))
        {
          o = next;
        }
        else
        {
          result = null;
          return false;
        }
      }

      result = o;
      return true;
    }
  }
}

