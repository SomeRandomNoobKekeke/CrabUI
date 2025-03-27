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
  /// <summary>
  /// Class for easy parsing and serialization of anything
  /// </summary>
  public class CUIParser
  {
    public static object DefaultFor(Type T) => Activator.CreateInstance(T);

    public static T Parse<T>(string raw) => (T)Parse(raw, typeof(T));
    public static object Parse(string raw, Type T, bool verbose = true)
    {
      if (raw == null) return null;
      if (T == typeof(string)) return raw;

      if (T.IsPrimitive)
      {
        MethodInfo parse = T.GetMethod(
          "Parse",
          BindingFlags.Public | BindingFlags.Static,
          new Type[] { typeof(string) }
        );

        try
        {
          return parse.Invoke(null, new object[] { raw });
        }
        catch (Exception e)
        {
          if (verbose)
          {
            CUI.Warning($"CUIParser failed to parse [{raw}] into primitive type [{T}]");
          }
          else throw e;

          return DefaultFor(T);
        }
      }

      if (T.IsEnum)
      {
        try
        {
          return Enum.Parse(T, raw);
        }
        catch (Exception e)
        {
          if (verbose)
          {
            CUI.Warning($"CUIParser failed to parse [{raw}] into Enum [{T}]");
          }
          else throw e;

          return DefaultFor(T);
        }
      }

      if (!T.IsPrimitive)
      {
        MethodInfo parse = null;
        if (CUIExtensions.Parse.ContainsKey(T))
        {
          parse = CUIExtensions.Parse[T];
        }
        else
        {
          parse = T.GetMethod(
            "Parse",
            BindingFlags.Public | BindingFlags.Static,
            new Type[] { typeof(string) }
          );
        }

        try
        {
          return parse.Invoke(null, new object[] { raw });
        }
        catch (Exception e)
        {
          if (verbose)
          {
            CUI.Warning($"CUIParser failed to parse [{raw}] into [{T}]");
          }
          else throw e;

          return DefaultFor(T);
        }
      }

      return DefaultFor(T);
    }

    public static string Serialize(object o, bool verbose = true)
    {
      if (o.GetType() == typeof(string)) return (string)o;

      MethodInfo customToString = CUIExtensions.CustomToString.GetValueOrDefault(o.GetType());
      string result = null;

      try
      {
        result = customToString == null ? o.ToString() : (string)customToString.Invoke(null, new object[] { o });
      }
      catch (Exception e)
      {
        if (verbose) CUI.Warning($"CUIParser failed to serialize object of [{o.GetType()}] type");
        else throw e;
      }

      return result;
    }
  }
}
